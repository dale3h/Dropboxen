# Dropboxen

Dropboxen enables you to run multiple Dropbox clients concurrently by
automatically launching multiple instances of `Dropbox.exe` as different
Windows users so Dropbox will store the shared files in each unique Windows
user directory.

## Usage Instructions

Once downloaded, unzip the files to your Dropbox installation folder. The
default Dropbox installation folder is `C:\Program Files\Dropbox\`.

Open `Dropboxen.xml` in Notepad (or your favorite text editor) and modify the
username and password for each account entry. You have to create the accounts
manually in Windows before using this tool. Copy and paste an account line
to add more accounts, or just type it out.

### Windows 7

**Dropboxen does NOT CURRENTLY WORK on Windows 7 and fails with an "Access is denied" error message (confirmed 30 DEC 2009). Instead, on Windows 7 or Vista you can run multiple instances of Dropbox simply using the `runas` command as seen [here][win7].**

### Windows XP

You will need to create a Windows user account on your computer for each Dropbox account that you will be using. For example, if you need to connect to 3 Dropbox accounts, you will create 3 Windows user accounts on your PC. Each user account will have its own `My Dropbox` directory in each user account's `My Documents` folder.

Windows user accounts can be added through the Windows control panel feature **User Accounts**. Launch the Windows control panel, click **Create a new account** and enter the name of your new Windows account, make sure it has administrator rights, and then click **Create Account**. Now set a password for this Windows user account by clicking on the account and selecting **Create a password** or else this won't work. I chose ` ` (just a single space character) as my password for all accounts just to make it easier on myself, it just cannot be blank.

*__IMPORTANT:__ You must log into each new account once for the proper directories to be created.*

The process on Vista is similar, just a few things are different.

`Dropboxen.xml` format:

```xml
<?xml version="1.0"?>
<root>
  <accounts>
    <account username="(windows user account name)" password="(windows user account password)" />
    <account username="(windows user account name 2)" password="(windows user account password 2)" />
  </accounts>
</root>
```

When I tested this proof of concept, I created three extra Windows usernames: `Dropbox001`, `Dropbox002` and `Dropbox003`. What Windows username(s) you choose is entirely up to you. Here's my `Dropboxen.xml` file:

```xml
<?xml version="1.0"?>
<root>
  <accounts>
    <account username="Dropbox001" password=" " />
    <account username="Dropbox002" password=" " />
    <account username="Dropbox003" password=" " />
  </accounts>
</root>
```

*__IMPORTANT:__ The `Dropbox001` and other usernames specified in the XML file are __WINDOWS__ usernames, not Dropbox usernames!*

This program requires .NET Framework 3.5 to be installed.

[win7]: http://semi-legitimate.com/blog/item/multiple-dropbox-instances-on-windows-7
