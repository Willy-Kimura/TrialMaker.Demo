# TrialMaker: Trial Licensing for .NET Applications

[![Purchase via Gumroad](https://img.shields.io/badge/Install-NuGet-green.svg)](https://www.nuget.org/packages/TrialMaker.Licensing.Preview/) [![Purchase via Gumroad](https://img.shields.io/badge/Get-License-orange.svg)](https://gum.co/QQejI) [![Purchase via Gumroad](https://img.shields.io/badge/Get-Sources-yellow.svg)](https://gum.co/qmWSh) ![License](https://img.shields.io/badge/License-Proprietary-blue.svg)

[TrialMaker](https://gum.co/QQejI) is a premium library that provides trial licensing for .NET applications.  It provides a simplified and easy way to integrate secure free-trial license generation and copy protection features. It also supports premium offline license generation for expired free-trials with its own **License Generator** utility.

<div align="center">

![trial-maker-logo](/Assets/Icons/trial-maker-midres.png)

</div>

# Installation

To install via the [NuGet Package Manager](https://www.nuget.org/packages/TrialMaker.Licensing.Preview/) Console, run:

> `Install-Package TrialMaker.Licensing.Preview`

# Features

- Supports [.NET Framework 4.0](https://www.microsoft.com/en-us/download/details.aspx?id=17718) and higher.
- Super easy integration with signed and secured library.
- Provides a simplified yet powerful licensing model as compared to other libraries.
- Generates secure free-trial licenses with configurable trial periods.
- Encrypts licenses with strong military grade AES-256 encryption.
- Generates unique Hardware IDs per customer for license validation.
- Generates hardware-locked trial and premium licenses based on clients' Hardware IDs.
- Generates unique license keys based on clients' Hardware IDs.
- Supports activation of single and multiple devices with individual licenses.
- Organizes multiple products and their licenses automagically.
- Prevents multiple free-trial uses with its Proof Of Use (POU) feature.
- Determines whether clients have backdated their System Date/Time even when offline.
- Ability to restrict or limit the number of trial uses, e.g. 12 times (daily or till expiry).
- Ability to restrict or limit the usage time per running instance.
- Provides support for premium license activation using its own License Generator utility.
- Lets you provide a direct link to your product's purchase page that can be launched once a free-trial expires.
- Includes other additional properties and methods for handling licenses and license validation tasks.
- Very low memory footprint and impressively fast license validation tasks (refer to demo).
- Designed for both lightweight and enterprise applications.

## ✔️ Security checks

- All the core classes have been sealed to prevent inheritance.
- The library has been strong-named, obfuscated, and signed to prevent tampering.
- Licenses are generated using a secured cryptographic key to prevent any third-party license regeneration.
- Each product requires a unique *Product ID*, preventing anyone from recreating a similar product or its derived licenses. This also prevents anyone with the sources from generating licenses of a particular product.
- The Proof Of Use (POU) feature prevents multiple free-trials once licenses have expired.
- The License Generator utility (included on purchase) has also been secured and signed to prevent disassembling.
- The complete [source code](https://gum.co/qmWSh) is also provided separately for developers or teams who would like to extend the library with features such as server license validation, activation, and management.

# **Demo**

TrialMaker includes a sample C# demo project that will help you easily get started. Below is a sample preview of the running demo and the License Generator utility in-part:

> *To know more about the License Generator utility, please head over to the section [Activating Premium Licenses](#activating-premium-licenses).*

![trial-maker-demo](/Assets/Screenshots/trial-maker-demo.gif)

## 🔑 Demo limitations

TrialMaker's demo library let's any interested developers test its core features. Once satisfied, one can go ahead and purchase the [premium](https://gum.co/QQejI) version that removes all limitations, allowing you build distributable and production-ready applications.

The demo limitations include:

- A nag dialog appears per-build, both at design-time and runtime.
- Maximum of 7 runtimes per license.
- Maximum of 30 trial days per product configuration.
- A default product ID of `1234` is given for every licensed product.
- Library is not strong-named and signed.

To get the full-featured library, please consider [purchasing a license](https://gum.co/QQejI).

# Usage

First, ensure you import the library's core namespaces after installation:

```c#
using WK.Libraries.TrialMakerNS;
using WK.Libraries.TrialMakerNS.Models;
```

...then instantiate the core class and add the following code:

```c#
var tm = new TrialMaker();

// Add your product's information.
tm.ProductInfo = new ProductInfo()
{
    ID = "#MyVeryUniqueProductID#"               // A unique product ID string.
    Name = "My App",                             // Your product's name.
    Owner = "My Company",                        // Your company or alias name.   
    TotalDays = 30,                              // How many trial days?
    PurchasePage = "https://myapp.com/purchase"  // Your product's purchase page (optional).
};

// This validates an existing license or creates a new one.
License lic = tm.Validate();

if (lic.Status == LicenseStatus.Active)
{
    if (lic.Type == LicenseTypes.FreeTrial)
    {
        // Free trial license.
    }
    else if (lic.Type == LicenseTypes.Premium)
    {
        // Premium license.
    }
}
else if (lic.Status == LicenseStatus.Expired)
{
    // The license has expired.
}
else if (lic.Status == LicenseStatus.Invalid)
{
    // The installed license is invalid.
    // Use 'ValidationErrors' property to determine which errors were captured.
    // You can choose to exit your application to prevent usage.
}
```

Simple huh! That's just about it for the basic implementation of licensing in your apps.

Now let's break it down to understand what happens in each step...

## Breakdown

Please note that in all our examples, we will be using the `tm` variable which is an instance of the `TrialMaker` class.

### 1. Adding product information

All licenses created will require the following information:

1. A unique product ID.
2. The product name.
3. The product owner, team or company.
4. The number of trial days.
5. (*Optional*) A purchase link to direct potential clients.

With this information, licenses can be validated and verified for use by any client.

The product `ID` is an important property since it uniquely sets it apart from all other products. Ensure you store the product ID somewhere safe e.g. in a locally encrypted database or in your project's **Resources**. If you'll be storing it inside your project's Resources, please ensure you obfuscate the Resources before deployment.

Here's a code snippet:

```c#
tm.ProductInfo = new ProductInfo()
{
    ID = "#MyVeryUniqueProductID#"               // A unique product ID string.
    Name = "My App",                             // Your product's name.
    Owner = "My Company",                        // Your company or alias name.
    TotalDays = 30,                              // How many trial days?
    PurchasePage = "https://myapp.com/purchase"  // Your product's purchase page (optional).
};
```

Also and more importantly, if you have the *License Generator* utility and will be generating premium licenses for clients, ensure you provide the same product name and ID when adding your product.

### 2. Validating licenses

This is the core method that lets you validate any new or existing licenses generated for your clients. It allows for both **creation** and **validation** of licenses thus simplifying creation of new licenses or, at a similar level, validation of existing ones.

You only need to add one line of code:

```c#
License lic = tm.Validate();  // A TrialMaker license is returned after validation.
```

From here, we proceed to validating the license based on the returned `License` object.

#### The `License.Status` property

The most crucial section in the returned `License` object is the `Status` property. There are currently **3** license status options provided:

- `Active` - The product license is installed and active.
- `Expired` - The installed product license has expired.
- `Invalid` - The installed product license or any external variable related to it is invalid, e.g. System Date.

To determine the type of license installed, use the `License.Type` property:

```c#
if (lic.Status == LicenseStatus.Active)
{
    if (lic.Type == LicenseTypes.FreeTrial)
    {
        // Free trial.
    }
    else if (lic.Type == LicenseTypes.Premium)
    {
        // Premium.
    }
}
```

Premium licenses can be generated using the *License Generator* utility. For more info, please checkout the section [Activating Premium Licenses](#activating-premium-licenses).

To activate a premium license, use the `Activate(string license)` method. This method accepts a secured license string as an argument:

```c#
var lic = tm.Activate("provide the secured license");

if (lic.Status == LicenseStatus.Active)
{
    // The license has been activated and installed.
}
else if (lic.Status == LicenseStatus.Invalid)
{
    // Use the 'ValidationErrors' property to determine which errors were raised.
    string errors = string.Join(",\n", tm.ValidationErrors);
    MessageBox.Show(errors);
}
```

You can also check whether the user is trying the application for the first time using the `License.FirstTime` property. This could be useful if you wish to welcome any potential clients or even showcase premium features to encourage upgrading to the premium version:

```c#
if (lic.Status == LicenseStatus.Active)
{
    if (lic.Type == LicenseTypes.FreeTrial)
    {
        if (lic.FirsTime)
        {
            // This is a first-time free trial user.
        }
    }
}
```

In place of the `License.FirstTime` property which is only available after validation, you can also use the library's `LicenseExists` property:

```c#
if (tm.LicenseExists == false)
{
    // Show e.g. a welcome dialog.
}
```

#### Checking validation errors

For licenses deemed invalid, we can make use of the `ValidationErrors` property that TrialMaker provides to determine the errors raised upon validation. The `ValidationErrors` property stores a collection of all possible errors captured during validation. They are:

| Error                | Description                                                  |
| -------------------- | ------------------------------------------------------------ |
| `SystemBackdated`    | The System Date/Time has been backdated.                     |
| `LicenseEmpty`       | The license is empty.                                        |
| `LicenseCorrupted`   | The license is corrupted.                                    |
| `InvalidHardwareID`  | The device Hardware ID does not match the Hardware ID registered with the license. |
| `InvalidProductID`   | The license's product ID does not match the original product ID. |
| `InvalidProductName` | The license's product name does not match the original product name. |

These errors have been specifically provided to enable copy-protection and promote license integrity when distributing your applications.

As a safety mechanism, if any error is raised in your application, you can exit it to prevent further use until the errors(s) have been resolved. You can also provide an error reporting feature that allows clients to submit errors if they were not malicious in nature. 

Here's an example displaying the various errors that may have been raised:

```c#
if (lic.Status == LicenseStatus.Invalid)
{
    string errors = string.Join(",\n", tm.ValidationErrors);
    MessageBox.Show(errors);
    
    // You can choose to exit your application to prevent usage.
    // Application.Exit();
}
```

To verify whether a specific error was raised, use the `Contains` method as provided in list collections:

```c#
if (lic.Status == LicenseStatus.Invalid)
{
    if (tm.ValidationErrors.Contains(ValidationErrors.SystemBackdated))
    {
        MessageBox.Show("Please do not backdate your system.");
        // Application.Exit();
    }
}
```

You can also refer to the sample demo for a live working preview.

#### The `License` class

Below are the properties in a TrialMaker `License` class:

| Property         | Description                                                  |
| ---------------- | ------------------------------------------------------------ |
| `Type`           | Gets or sets the license type.                               |
| `Status`         | Gets or sets the license status.                             |
| `Company`        | Gets or sets the licensed owner or company.                  |
| `Product`        | Gets or sets the product name.                               |
| `ProductID`      | Gets or sets the unique product ID.                          |
| `LicenseKey`     | Provides an auto-generated free-trial license key based on the MD5 hash of the customer's Hardware ID. |
| `HardwareIDs`    | Gets or sets the list of devices that can be activated based on their Hardware IDs. |
| `Activations`    | Gets the number of activations available based on the list of device Hardware IDs. |
| `ActivationDate` | Gets or sets the date the license was created and activated. |
| `ExpiryDate`     | Gets the date the license is set to expire.                  |
| `LastUsed`       | Gets or sets the date the license was last used. This is only updated if the `TrialMaker.TrackUsage` property is set to true. |
| `TotalDays`      | Gets or sets the total number of days assigned to the customer. |
| `RemainingDays`  | Gets the number of days remaining before the license expires. |
| `IsValid`        | Gets a value indicating whether the license is valid based on a device's Hardware ID. |
| `FirstTime`      | Verifies whether this is a first-time user, meaning their license has been created for the first time. |
| `Uses`           | Gets the number of times the license has been used. This is synonymous with the number of times the application has been run. However once a premium license has been activated, the usage count will be reset to zero to initiate a new counting. This is only updated if the `TrialMaker.TrackUsage` property is set to true. |
| `Device`         | Gets the user device information.                            |

### 3. Tracking usage of licenses

#### Tracking number of uses

As pointed out earlier, with TrialMaker, you can easily track the number of uses per customer license. We do this by enabling the property `TrackUsage` in TrialMaker and using the `License.Uses` property. You can also get when last the license was used using the `License.LastUsed` property which is of a `DateTime` type.

Here's an example:

```c#
// Enable usage tracking.
tm.TrackUsage = true;

// Set maximum usage count.
int maxUses = 12;

if (lic.Status == LicenseStatus.Active)
{
    if (lic.Uses == maxUses)
    {
        MessageBox.Show(
            "You have exceeded the no. of uses." +
            "Please purchase a full version.");
    }
    else
    {
        MessageBox.Show($"You have {12 - lic.Uses} remaining.");
    }
}
```

You can also check the last used date/time using the `License.LastUsed` property:

```c#
MessageBox.Show($"Last Used: {lic.LastUsed}");
```

#### Tracking usage time

To track how much time is being used, simply enable the property `TrialMaker.TrackTime`. This will then allow you to use the `TrialRuntime` event that tracks the application's running timespan:

```c#
// Enable time tracking.
tm.TrackTime = true;

// Add time tracking event.
tm.TimeTracking += TimeTracking;

private void TimeTracking(object sender, TrialMaker.TimeTrackingEventArgs e)
{
    string time = e.TimeSpan.ToString(@"hh\:mm\:ss");
}
```

With this you can choose to limit how much time clients will use your application in free-trial mode:

```c#
private void TimeTracking(object sender, TrialMaker.TimeTrackingEventArgs e)
{
    if (e.TimeSpan.Hours == 2)
    {
        // If time used exceeds 2 hours, inform the user to upgrade.
        MessageBox.Show(
            "Your trial time limit has reached." + 
            "Please purchase a full version.");
	}
}
```

As a side note, always ensure you inform your users if at all you prefer to restrict how much time or the number of uses for your application(s) in free trial mode.

### Additional features

#### 1. Getting the Hardware ID

You can get a client's machine Hardware ID using the static `TrialMaker.HardwareID` property:

```c#
string hid = TrialMaker.HardwareID;
```

This property auto-generates a 36-character unique machine code based on the client machine's hardware properties.

Here's a machine code sample: `LIITOJVC-HREOTRVU-NTHNQUWT-UQABNVGK-BGXH`

#### 2. Checking if a license exists

To check whether a license exists (free-trial or premium), simply use the `LicenseExists` property:

```c#
bool exists = tm.LicenseExists;
```

#### 3. Checking for Network availability

You can determine whether an Internet connection is available using the `IsNetworkAvailable` property:

```c#
bool netAvailable = tm.IsNetworkAvailable();
```

#### 4. Checking if system date is backdated

To manually check if the current system date is backdated, use the `IsSystemBackdated()` method:

```c#
bool isBackdated = tm.IsSystemBackdated();
```

#### 5. Checking if system date is correct

To manually check if the current system date is correct (up-to-date), use the `IsSystemDateCorrect()` method:

```c#
bool isDateCorrect = tm.IsSystemDateCorrect();
```

#### 6. Opening the purchase page

To open the product's purchase page as registered in the `ProductInfo` property, use the `OpenPurchasePage()` method:

```c#
tm.OpenPurchasePage();
```

This will open the link using the default browser.

You can also open other links using the method `OpenLink(string url)`.

#### 7. Displaying a product title

TrialMaker helps model a title message for use in your application using the `ModelProductTitle()` method:

![model-product-title](/Assets/Screenshots/model-product-title.png)

```c#
string title = tm.ModelProductTitle();

// Format: "Product (LicenseType)", e.g. "My App (30-day free trial)" or "My App (Premium)"
```

#### 8. Displaying a license expiration message

TrialMaker also helps model an expiration message using the `ModelExpirationMessage()` method:

![model-product-title](/Assets/Screenshots/model-expiration-message.png)

```c#
string title = tm.ModelExpirationMessage();

// Format: "Evaluation period expires on {dddd dd MMMM, yyyy}" 
// e.g. "Evaluation period expires on Wednesday 13th August, 2020"
```

## Activating premium licenses

> License Generator requires activation. Please ensure you provide your valid *purchase email* and *license key* after purchase.

TrialMaker includes an offline and powerful license generation utility that lets you generate premium licenses for converting free-trial users to premium clients. You can provide single device activations or multi-device activations, meaning that a client can either receive a single license for one device or a single license for multiple devices:

![trial-maker-demo](/Assets/Screenshots/trial-maker-license-generator.png)

We will now discuss how the License Generator works...

### Features

- Generating hardware-locked licenses based on a client's Hardware ID.
- Generating subscription licenses by limiting the no. of days of use, e.g. 365.
- Managing premium clients and products for reference purposes and support.
- Supports single device activation and multiple device activations, meaning clients can either receive a single license for one device or a single license for multiple devices.

### Usage

The utility has four main sections:

1. **Device Information**: This is where you provide the client's generated Hardware ID that will be used to lock the license to their machine. You can also provide a list of Hardware IDs from the various devices a client will be using the application:

   ![hid-section](/Assets/Screenshots/hid-section-2.png)

2. **License Information**: Here, you will provide the name of the product being licensed, the client's name or company, and (optionally) choose to limit the number of days for using the license - this may be necessary if your product is subscription-based, e.g yearly:

   ![hid-section](/Assets/Screenshots/licensing-section.png)

   Here you can also add a product to the list of products you're currently selling to make it easier when licensing multiple clients. To do this, simply click on the *Add* icon button to the left of the products combo box; a dialog will appear allowing you to add your product(s) to the local db:

   ![hid-section](/Assets/Screenshots/add-edit-delete-products.gif)

   

3. **License Generation**: This section is mainly for generation of licenses based on the provided inputs. You can also copy the license generated and/or save the license as a file to send to the client for activation. Sending licenses as files is a preferred option to make the activation process easier for clients.

   ![hid-section](/Assets/Screenshots/lc-generation-section.png)

4. **Clients Database**: This section mainly covers the clients management aspect. Whenever you generate a premium license, it is automatically added to the local *Clients.db* database located in the same folder as the utility. In this section, you can view your list of clients, search for a particular client, view their license details, and/or delete their records:

   ![hid-section](/Assets/Screenshots/clients-db-section.png)

   To search for clients, use the search input located at the top. To specify the search option, use the combo box to the right of the search input:

   ![hid-section](/Assets/Screenshots/search-clients.gif)

You can expand or collapse the Clients Database section using the *expand/collapse* icon: ![search-clients](/Assets/Screenshots/expand-collapse-icon.png)

That's about it on License Generator. More updates and improvements are sure to come with every release.

# License

TrialMaker and its sources are governed under a Proprietary License. The demo however is under an MIT license.

Feel free to reach out via email for any consultations: wilskym[at]live[dot]com.

You can also get the [complete source code](https://gum.co/qmWSh) if you'd love to build your own thing or extend the product even further.

*Made with* 💛 *by Willy Kimura*