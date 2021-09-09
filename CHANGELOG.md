# Changelogs for TweakUtility

## 1.1.2 (September ?? 2021)
Add later

## 1.1.1 (October 27th 2019)
Internal version, never released though it was pushed out by accident.

## 1.1.0 (August 15th 2019)
![image](https://user-images.githubusercontent.com/45898787/132110246-c10652b5-c7fa-4b55-ad4c-9e7553611d99.png)

Entered beta versions. Expect more polish in the following builds.

- **Completed** classic theme colors page.
- **Completed** Internet Explorer page locations. (Thanks @chaziz94)
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

## 1.0.99 (August 2019)
![readme](https://user-images.githubusercontent.com/45898787/132110217-cc5c71fc-dd50-44f3-b1be-420d57432197.png)

Internal versions of 1.1.0, never released.

## 1.0.98 (July 30th 2019)

![image](https://user-images.githubusercontent.com/45898787/132110206-1450054f-9c0a-453a-9928-95010d690d2c.png)

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

## 1.0.96 (July 22nd 2019)

![image](https://user-images.githubusercontent.com/45898787/132110154-878eccdf-fb3c-41c7-9dc9-a3736511d17c.png)

Internal version, it was a sneak peek for 1.0.98

- Themes are deprecated, These weren't any use at all. Never forget Plex theme.
- Settings are deprecated. This was only for Themes.
- The sidebar now has icons! These aren't fully done, yet. It should be done by 1.0.98.
- Some easter eggs are added. They are comments, They aren't in the compiled builds 😛 
- Disk Cleanup page is added, Now you can clean more directories!
- And other changes i don't notice

## 1.0.94 (July 21st 2019)

First public release of TweakUtility.

![image](https://user-images.githubusercontent.com/45898787/132110149-8ff067c0-535f-4c63-b90e-1718dc58e3c9.png)

- Redesigned the UI

### Reddit post

We've skipped Build 93.

Also we first released at 1.0.94, because PF94, being me.

Oh and no, Don't move your TweakUtility exe file somewhere else. as it requires "Newtonsoft JSON"

## 1.0.93 (July 2019)

Unknown, skipped in favor of 1.0.94.

## Pre-1.0.93 (July 2019)

Internal versions that were never released because they were super prototypical.

![image](https://user-images.githubusercontent.com/45898787/132110268-b4807019-eca8-435f-92e0-352d8f8f1653.png)

![image](https://user-images.githubusercontent.com/45898787/132110271-ce836574-8f5f-476b-990b-85ea571bc1f4.png)

![image](https://user-images.githubusercontent.com/45898787/132110476-826c0d8b-6e6c-4198-aa91-c169843b6df5.png)
