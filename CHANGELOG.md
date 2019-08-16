# Changelogs for Tweak Utility

## 1.1.0
Entered beta versions. Expect refining of the new features in the following builds.

- **Completed** classic theme colors page.
- **Completed** Internet Explorer page locations. (Thanks @PF94)
- **Added** a pretty bad and cringy easter egg. (this version only!)
- **Added** extensions.
- **Added** backups.
- **Added** credits.
- **Added** Windows Media Player deskband installation.
- **Added** searching pages and tweaks.
- **Added** Windows 10 classic UI options.
- **Changed** snipping tool page.
- **Removed** revision numbers on the build number

### Code Changes
- **Added** new attributes for checking various things.
- **Changed** attributes used for tweak pages and entries.
  - **Removed** default values for the time being.
- **Removed** unused resources due to dynmaic retrieval of icons.
- **Added** progress indicator class.
- Refactoring

### Unfinished
- Startup items
- Registry editor
- Translations
- Windows 7 API support

## 1.0.98

- **Added new installer**
- **Added** new MSN Messenger page (incomplete).
- **Added** support, for hiding pages based if registry keys exist. (RegistryKeyRequiredAttribute)
- **Added** controls, for adding, removing, refreshing disk cleanup items. (DiskCleanupPage)
- **Added** helper method, for reading booleans. (RegistryHelper)
- **Added** string resources, for keeping text in one place. (Resources)
- **Added** new "Classic Theme Colors" sub page (incomplete) to the Customization page. 
- **Added** dynamic page icons, for Advanced (control panel) and Customization.
- **Added** new RefreshRequiredAttribute types, being process restart and unknown (basically unimplemented refresh).
- **Moved** the IsSupported query, from OperatingSystemVersions to OperatingSystemSupportedAttribute.
- **Changed** the way how SplashForm loads tweak pages, now it checks attributes before creating instances of them.
- **Changed** native helpers, to allow extracting images (left over/unused), as well as minor name refactoring.
- **Changed** the "Delete OneDrive trails" to "Uninstall OneDrive" option in the Windows 10 page. (more deletion of trails, automatic uninstall etc.)
- **Improved** some reflection methods to check for null parameters (incomplete).
- **Improved** dynamic page icon retrival for Snipping Tool.
- **Fixed** the MainForm from appearing, when the SplashForm was closed by the user.  
- **Removed** user agent option from Internet Explorer, as it didn't seem to change anything (dead registry value).
  - **Moved** internal page locations to Internet Explorer page, therefore, removing the Tabs sub page.
- Refactoring like always
