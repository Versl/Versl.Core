# Versl Core
An open source, mobile app framework built on [Microsoft Xamarin](https://dotnet.microsoft.com/apps/xamarin/) and [Google Firebase](https://firebase.google.com/).

# **Getting Started**
The following guide will help you get up and running. We’ve made it as easy as possible for even non-developers to launch a great mobile app.

## **Professional Setup**
Want to push the easy button and have our team set up your app? Contact our sales team at [sales@myversl.com](mailto:sales@myversl.com) for assistance.

## Licensing and Support
We have two licenses available for Versl Core:

### Open Source
Versl Core is licensed under the [GNU GPL v3](https://opensource.org/licenses/GPL-3.0) and requires all derivatives to be distributed free and open source as well. Support is offered via our Github community.

### Commercial
Commercial licenses include support and can be purchased via our website: [https://myversl.com](https://myversl.com/). Contact our sales team at [sales@myversl.com](mailto:sales@myversl.com) for assistance.

## Requirements
-  [Google Firebase](https://firebase.google.com/) - the backend for your app. Offering database, push notifications, authentication, analytics, crash reporting services.
-  [Apple Developer](https://developer.apple.com/programs/) - you will need an Apple Developer account to release your app into the Apple App Store. [Click here to enroll](https://developer.apple.com/programs/enroll/).    
-  [Google Play](https://play.google.com/console/about/) - you will need a Google Play Developer account to release your app into the Google Play Store. [Click here to enroll](https://play.google.com/apps/publish/signup/).
All your files and folders are presented as a tree in the file explorer. You can switch from one to another by clicking a file in the tree.
- [Microsoft Visual Studio](https://visualstudio.microsoft.com/) - You will need [Microsoft Visual Studio](https://visualstudio.microsoft.com/vs/) or [Microsoft Visual Studio for Mac](https://visualstudio.microsoft.com/vs/mac/) to compile your app for iOS and Android.

# **Setup**
Follow the steps below to get up and running:

1.  Clone/fork this repository.    
2.  Generate Apple Certificates and Provisioning Profiles
	1.  Push certificate.
	2.  Developer provisioning.   
	3.  App store provisioning.
3.  Firebase
    1.	Create your account and enable:
		- Realtime Database
			- Optional: Import Firebase/default-firebase-database.json. This file contains the default database values to get you up and running quickly.
		- Authentication (Email/Password)
			- Templates: Edit the email templats to include your branding. These are the communication templates for password reset, email change, etc.
			- Custom Domain (Optional): Setup a custom domain for your Firebase instance.
		- Storage
		- Push
			- You will need the Apple Push certificate created in step 2.1.
		- Crashlytics
    2.  Spark plan (Optional)
		- We recommend the Spark plan for production instances. Spark enables database backups.
    3.  Publish the Firebase project in the Firebase folder. This project contains default rules and functions to support your app.
    7.  Create Apps (Project Settings/General)
		- Download config files for:
			-  iOS: GoogleService-Info.plist. Place in root of iOS project.
			-  Android: google-services.json. Place in root of Android project.
4.  App Icons  
	1. Generate from 1024x1024 png.
		1. Create a 1024x1024 png file featuring your logo.
		2. Open a command/terminal window and navigate to your project root.
		3. Execute cordova-res.
		4. App icon and launch screen assets are generated in the resources folder.
5. iOS Launch Screens
	- Update Resources/Assets.xcassets with generated launch screen assets.
6.	Update App Identifiers
	-	iOS: Info.plist
	-	Android: Project Settings > Android Application > Project Name
5.  Publishing
	- Use Archive For Publishing on each project to publish to corresponding app stores.

# Customization

Versl Core can be easily customized through dependency injection. MVVMCross enables all of this magic. Each of the platform projects contains a setup.cs file where views, viewmodels, and services are injected. Objects from Versl Core can be inherited and overridden or new components deriving from our interfaces can be created.

# Solution Structure

- Common
	- Versl.Core: The primary Versl Core functionality.
	- Versl.Firebase: All Firebase specific functionality.
- Versl.Android: Android platform project.
- Versl.Application: Xamarin forms .Net Core application.
- Versl.iOS: iOS platform project.

Versl.Core and Versl.Firebase are referenced from the platform projects as NuGet packages. These projects are included in the solution for customization and debugging purposes.

# Color System
The color system in Versl Core is based on Google's [Material Design Color System](https://material.io/design/color/the-color-system.html#color-usage-and-palettes).

- Primary: Header & Tabbar Background
- OnPrimary: Header & Tabbar Text
- PrimaryVariant: TabBar Unselected
- Secondary: CTA Buttons
- OnSecondary: CTA Button Icon
- SecondaryVariant: Media Bar Background
- Background: Page Backgrounds
- OnBackground: Page Text
- Surface: Menu Background
- OnSurface: Menu Text


# Open Source Libraries

Versl Core could not have been built without the following open source libraries. Love and thanks to the contributors of:

-   [MVVMCross](https://github.com/MvvmCross/MvvmCross)
-   [Step-Up-Labs.FiirebaseDatabase.Net](https://github.com/step-up-labs/firebase-database-dotnet)
-   [Step-Up-Labs.FiirebaseStorage.Net](https://github.com/step-up-labs/firebase-storage-dotnet)
-   [Step-Up-Labs.FirebaseAuthentication.Net](https://github.com/step-up-labs/firebase-authentication-dotnet)
-   [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json)
-   [Automapper](https://github.com/AutoMapper/AutoMapper)
-   [CrossGeeks.FirebasePushNotifications](https://github.com/CrossGeeks/FirebasePushNotificationPlugin)
-   [Baseflow.MediaManager](https://github.com/Baseflow/XamarinMediaManager)
-   [JamesMotemagno.MediaPlugin](https://github.com/jamesmontemagno/MediaPlugin)
-   [Luberda-Molinet.FFImageLoading](https://github.com/luberda-molinet/FFImageLoading)

# Contributing

We are happy to receive Pull Requests adding new features and solving bugs. As for new features, please contact us before doing major work. To ensure you are not working on something that will be rejected due to not fitting into the roadmap or ideal of the framework.