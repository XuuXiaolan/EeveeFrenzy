## Version 1.4.3
- Added config item to add a "show/hide" button to section headers. (#53)
- Adjusted header appearance slightly (smaller & will now add ellipses to sections that have too long of a name) (#53)
- Made it so string configs with "acceptable values" now correctly create dropdown config buttons automatically (#53)
- Added some methods for adding config files that were generated later than awake (such as LLL & LethalConstellations) (#53)
- Added config item to automatically hide config items at start when show/hide buttons are enabled. (#53)
- Changed most info logs to debug.

## Version 1.4.2
- Reverted changes to most config item's constructors from #42, as it was an unintentional breaking change. (#47)

## Version 1.4.1
- Exposing `BaseConfigItem#ApplyChanges`, `BaseConfigItem#CancelChanges`, and `BaseConfigItem#ChangeToDefault`
- Added optional Assembly argument for `LethalConfigManager#AddConfigItem`
- Marking dynamically loaded mod icons as non-readable to improve memory usage.

## Version 1.4.0
- Updating config appearances whenever a `ConfigEntry` is modified directly.
- Added `QueueCustomConfigFileForAutoGeneration` method, allowing the auto generation of configs for manually created config files.
- Added new options to `TextInputFieldOptions`:
  - `NumberOfLines` Sets how many lines a text field can have. Setting it to 0 means no limit.
  - `TrimText` When true, automatically removes empty space from the start and end of the text.
- Added new component `TextDropDownConfigItem`, which allows you to make a dropdown selector for strings (including configs with `AcceptableValueList`) (thank you @Kittenji)
- Adjusted colors of the menu to be darker.
- Added "Hide Lethal Config" config to disable LethalConfig in-game. This setting is only visible on the r2modman's config editor and is false by default.
- Fixed issue where a mod couldn't have more than one generic button (comparison would always think they're the same).
- Attempt to fix issue where configs would not be generated if a plugin's instance was null at the time of the generation.

## Version 1.3.4
- Fixed issue where some assemblies' depedencies could cause the UI to not load properly.
- Fixed issue with sliders/numeric inputs not using the `AcceptableValueRange`, this time for real.

## Version 1.3.3
- Fixed uncommon issue where closing LethalConfig would make the main menu/quick menu unusable due to not being deactivated and blocking the UI raycasts.
- Forcing LethalConfig to be set as the last sibling when it's opened. This ensures the menu will be shown above UI that may be added by other mods.

## Version 1.3.2
- Reverted type for slider and numeric input options from nullable.

## Version 1.3.1
- Fixed issue where duplicated fields were autogenerated when the section or name of the item was overriden
- Fixed issue where providing an options objects to sliders without setting their min/max values would not fallback into the `AcceptableValueRange` of the entry if one was present.

## Version 1.3.0
- Added new `GenericButtonConfigItem`, which allows you to create a button that provides you with a callback to run your own code when it's clicked.
- Added item's Section, Name, and Description overrides, which allows you to change what LethalConfig will show without changing the section, keys, or descriptions in your `ConfigEntry` directly.
- Fixed issue when trying to automatically read the mod's icon and description from mods installed manually with their dll at the root of the BepInEx's plugins folder. (@Rune580)
- Fixed exception caused in the quick menu when running LethalConfig with AdvancedCompany.

## Version 1.2.0
- LethalConfig now reads both the mod's icons and descriptions directly from its thunderstore manifest if one can be found. (@Rune580)
- Added methods to make LethalConfig skip the autogeneration for your entire mod, a config section, or individual configs. (@Rune580)
- Added callbacks to the ConfigItem's options to tell whether a field is modifiable or not. (@Rune580)
- Added LethalConfig to the in-game quick menu.
- Did a change to how the mod adds the menu button in the main menu, so it conflicts a bit less with other mods like LethalExpansion or IntroTweaks.

## Version 1.1.0
- Automatically generating mod entries and config items for all loaded mods that creates their own ConfigEntry. With this, mods technically don't have the need to necessarily have LethalConfig as a dependency unless they want to use specific components, mark certain settings as non-restart required, or want to set their mod icon and description.
- Adjusted some layout stuff.
- Fixed a bug with the initialization of sliders.
- Added customizable mod icons and mod descriptions.

## Version 1.0.1
- Added two new config types:
  - IntInputConfigItem (an integer text field)
  - FloatInputConfigItem (a float text field)
- Fixed missing default value in the enum dropdown's description.

## Version 1.0.0
- Initial release, which includes the following types:
  - IntSliderConfigItem
  - FloatSliderConfigItem
  - FloatStepSliderConfigItem
  - EnumDropDownConfigItem
  - BoolCheckBoxConfigItem
  - TextInputFieldConfigItem