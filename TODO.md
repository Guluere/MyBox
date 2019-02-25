* #### Asset Preset Postprocess Tool:
  * Tool to automatically assign Presets to Assets conditionally on Import stage with AssetPostprocessor
  * Conditions like "path contains/of type/with prefix/with postfix"
  * Solid tool to replace custom AssetPostprocessors with nice interface due to Preset feature :)
	
* #### MustBeAssigned may work with ScriptableObjects in project? 
  * Allow to disable this feature? Measure performance
  
* #### Multiscene asset
  * To save/load opened loaded/active scenes in editor
	
* #### IPrepare interface
  * To add custom logic to Behaviours
  * Execute those on Playmode and game build
  * To cache/calculate heavy stuff
	
* #### MyGizmosHandler
  * MyDebug.DrawText is working only in OnDrawGizmos :( 
  * I want to access OnDrawGizmos in non-MonoBehaviour scripts
  * MB with static access, with lazy initialization and HideAndDontSave?
    * EveryFrame subscription is heavy...
    * Push struct with IDraw and logic to draw with gizmos, remove pusded structs from MyGizmosHandler.OnDrawGizmos? Measure performance
    * Some way to draw every-frame Gizmos with system, that run only once per x seconds?
		
		
* #### AnimationCreator is pretty cool with simple Idle-Play or looping Idle animators. 
  * Is it possible to play animation without animator? 
    * https://github.com/Unity-Technologies/SimpleAnimation
	
* #### EmbeddedAnimationCreator to EmbeddedAssetCreator? 
  * It's cool to have, to pack related assets into one parent asset
  * Yeah, I know how parent assets, but how to unparent?
	
* #### MyBundleUtility is a mess. Might be useful
  * Tools to build bundles out of scenes (with multiscene solutions)
    * And handle bundles loading/unloading on scene load/unload?
		
* #### TemplatesCreator
  * Add a way to add templates as separate assets
  * There is no way to have MenuItems with runtime naming :(?
    * MenuItems is separate feature/wrapper for TemplateCreator?
    * WHOA! Figured it out. I may generate separate script with MenuItems in any selected by used folder. Find this script and get its path to regenerate if needed!
   
* #### Unique component is deprecated. GUID component is better. Remove it