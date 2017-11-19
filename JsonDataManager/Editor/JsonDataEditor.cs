﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityQuery;

public class JsonDataEditor : EditorWindow
{

	[MenuItem("Tools/Settings Editor", false, 50)]
	private static void Initialize()
	{
		var window = GetWindow<JsonDataEditor>();
		window.Show();
	}

	private void OnEnable()
	{
		hideFlags = HideFlags.HideAndDontSave;
		if (!SettingsLoaded) LoadSettings();
	}
	
	
	#region Load Settings

	private bool SettingsLoaded => !_settings.IsNullOrEmpty();

	private string SettingsFolder => Application.dataPath + "/" + JsonDataManager.SettingsFolder;
	private string[] SettingsFiles => Directory.GetFiles(SettingsFolder).Where(f => f.EndsWith(".json")).ToArray();

	private void LoadSettings()
	{
		_settings = GetValidSettingsData();
		_foldout = _settings.Select(c => true).ToArray();
		GUI.FocusControl(string.Empty);
	}

	private SettingsData[] GetValidSettingsData()
	{
		var settings = new List<SettingsData>();
		var settingsPaths = SettingsFiles;
		var hideAttributeType = typeof(HideInJsonEditorAttribute);

		for (var i = 0; i < settingsPaths.Length; i++)
		{
			var path = settingsPaths[i];
			var typeName = Path.GetFileNameWithoutExtension(settingsPaths[i]);
			if (string.IsNullOrEmpty(typeName)) continue;
			var settingsType = GetTypeByName(typeName);
			if (settingsType == null) continue;
			if (Attribute.IsDefined(settingsType, hideAttributeType)) continue;

			var settingsData = CreateInstance<SettingsData>();
			settingsData.Initialize(path);
			settings.Add(settingsData);
		}

		return settings.ToArray();
	}

	private Type GetTypeByName(string typeName)
	{
		return (from assembly in AppDomain.CurrentDomain.GetAssemblies()
			from type in assembly.GetTypes()
			where type.Name == typeName
			select type).SingleOrDefault();
	}

	private void CreateNewSettingsFile(string typeName)
	{
		var targetType = GetTypeByName(typeName);
		if (targetType == null || !targetType.IsSubclassOf(typeof(ScriptableObject))) return;

		bool exists = SettingsFiles.Any(f => f.Contains(typeName));
		if (exists) return;

		var instance = CreateInstance(targetType.Name);
		var jsonString = JsonUtility.ToJson(instance, true);
		var filePath = Path.Combine(SettingsFolder, typeName + ".json");

		File.WriteAllText(filePath, jsonString);
		AssetDatabase.Refresh();

		LoadSettings();
	}

	#endregion


	private bool IsChanged => _settings != null && _settings.Any(s => s.Changed);

	[SerializeField] private SettingsData[] _settings;
	[SerializeField] private bool[] _foldout;

	private Vector2 _scroll;
	private string _newSettingsFile;


	private void OnGUI()
	{
		CheckData();

		DrawSaveLoadButtons();

		DrawSettingsInspectors();

		DrawCreateSettingsFileButton();
	}

	private void CheckData()
	{
		if (_settings.IsNullOrEmpty() ||
		    _settings.Any(s => s == null))
		{
			LoadSettings();
		}
	}

	#region Save Load Buttons

	private void DrawSaveLoadButtons()
	{
		var dirty = IsChanged;
		GUI.enabled = dirty;
		var color = GUI.color;

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		using (new EditorGUILayout.HorizontalScope())
		{
			EditorGUILayout.Space();
			if (dirty) GUI.color = Color.green;
			if (GUILayout.Button("Save Changes", EditorStyles.toolbarButton)) SaveChanges();
			GUI.color = color;
			EditorGUILayout.Space();
			if (GUILayout.Button("Reset Changes", EditorStyles.toolbarButton)) ResetChanges();
			EditorGUILayout.Space();
		}
		EditorGUILayout.Space();

		GUI.enabled = true;
	}

	private void SaveChanges()
	{
		foreach (var setting in _settings)
		{
			if (setting.Changed) setting.SaveChanges();
		}
	}

	private void ResetChanges()
	{
		foreach (var setting in _settings)
		{
			if (setting.Changed) setting.ResetChanges();
		}

		Repaint();
	}

	#endregion


	#region Settings inspectors

	private void DrawSettingsInspectors()
	{
		using (var scroll = new EditorGUILayout.ScrollViewScope(_scroll))
		{
			_scroll = scroll.scrollPosition;

			for (var i = 0; i < _settings.Length; i++)
			{
				var settings = _settings[i];
				
				_foldout[i] = EditorGUILayout.Foldout(_foldout[i], settings.Name);

				if (_foldout[i])
				{
					settings.DrawInspector();
				}

				MyGUI.DrawLine(MyGUI.Gray, true);
			}
		}
	}

	#endregion


	#region Create Settings File

	private void DrawCreateSettingsFileButton()
	{
		using (new EditorGUILayout.HorizontalScope())
		{
			_newSettingsFile = EditorGUILayout.TextField(_newSettingsFile);
			if (GUILayout.Button("Create Setting File", EditorStyles.toolbarButton))
			{
				CreateNewSettingsFile(_newSettingsFile);
				_newSettingsFile = string.Empty;
			}
		}
	}
	
	#endregion


	#region Settings Data

	[Serializable]
	private class SettingsData : ScriptableObject
	{
		public string Name => _name;

		public bool Changed => _changed;

		[SerializeField] private bool _changed;
		
		[SerializeField] private string _name;
		[SerializeField] private ScriptableObject _so;
		[SerializeField] private Editor _editor;
		[SerializeField] private string _filePath;


		private void OnEnable()
		{
			hideFlags = HideFlags.HideAndDontSave;
		}

		public void DrawInspector()
		{
			EditorGUI.BeginChangeCheck();
			_editor.DrawDefaultInspector();
			if (EditorGUI.EndChangeCheck()) _changed = true;
		}
		
		public void Initialize(string path)
		{
			_filePath = path;
			_name = Path.GetFileNameWithoutExtension(path);
			
			LoadFromFile();
		}

		public void ResetChanges()
		{
			_changed = false;

			LoadFromFile();
		}

		public void SaveChanges()
		{
			_changed = false;
			var jsonString = JsonUtility.ToJson(_so, true);

			File.WriteAllText(_filePath, jsonString);

			AssetDatabase.Refresh();
		}
		private void LoadFromFile()
		{
			_so = LoadObject(_filePath);
			_editor = Editor.CreateEditor(_so);

			_so.hideFlags = HideFlags.DontSave;
			_editor.hideFlags = HideFlags.HideAndDontSave;
		}

		private ScriptableObject LoadObject(string path)
		{
			var contents = File.ReadAllText(path);
			var obj = CreateInstance(Name);
			JsonUtility.FromJsonOverwrite(contents, obj);

			return obj;
		}
		

	}

	#endregion

}