# VPN Installation Guide

This guide explains how to install the Palo Alto Networks GlobalProtect VPN client, import the required RootCA certificate, and connect to the VPN portal.

## Required Files

Before starting, make sure you have:

- `RootCA.crt` or the certificate ZIP that contains `RootCA.crt`
- GlobalProtect installer MSI, for example `GlobalProtect64.msi` or the provided `.msi` file
- VPN portal address: `baranyai2.ddns.net`
- VPN username and password

Do not publish VPN passwords in documentation. Share credentials through a private channel.

## 1. Import the RootCA Certificate

### 1.1 Open the certificate

Open `RootCA.crt`. Windows may show an **Open File - Security Warning** because the certificate file came from the internet or a ZIP file.

Click **Open**.

### 1.2 Start certificate installation

The certificate window may show that the CA root certificate is not trusted yet.

Click **Install Certificate...**.

### 1.3 Choose Local Machine

In the Certificate Import Wizard, select:

```text
Local Machine
```

Then click **Next**.

Windows will ask for administrator permission through User Account Control. Click **Yes**.

### 1.4 Choose the certificate store

Select:

```text
Place all certificates in the following store
```

Then click **Browse...**.

In the store list, choose:

```text
Trusted Root Certification Authorities
```

Then click **OK**.

The selected certificate store should now show:

```text
Trusted Root Certification Authorities
```

Click **Next**.

### 1.5 Finish the import

Review the settings and click **Finish**.

When Windows confirms the import, click **OK**.

## 2. Install GlobalProtect

### 2.1 Run the MSI installer

Run the provided GlobalProtect `.msi` file.

Windows User Account Control may ask:

```text
Do you want to allow this app to make changes to your device?
```

The verified publisher should be:

```text
Palo Alto Networks, Inc
```

Click **Yes**.

### 2.2 Select installation folder

Keep the default installation folder unless your administrator told you otherwise:

```text
C:\Program Files\Palo Alto Networks\GlobalProtect\
```

Click **Next**.

### 2.3 Confirm installation

Click **Next** to start the installation.

### 2.4 Complete installation

When the installer says **Installation Complete**, click **Close**.

## 3. Connect to the VPN

### 3.1 Open GlobalProtect

Open the GlobalProtect application.

If it shows **Not Connected**, enter the portal:

```text
baranyai2.ddns.net
```

Click **Connect**.

### 3.2 Enter login credentials

Enter the provided VPN username and password.

Example username, if provided by the administrator:

```text
Guest
```

Click **Connect**.

### 3.3 Verify connection

When the VPN is connected, GlobalProtect shows:

```text
Connected
baranyai2.ddns.net
Best Available Gateway
```

## 4. Troubleshooting

### Certificate still says it is not trusted

Reinstall the certificate and make sure it is placed in:

```text
Trusted Root Certification Authorities
```

Also make sure you selected **Local Machine**, not **Current User**.

### GlobalProtect cannot connect

Check:

- the portal address is exactly `baranyai2.ddns.net`
- the certificate was imported successfully
- the username and password are correct
- the computer has internet access
- Windows firewall or antivirus is not blocking GlobalProtect

### The MSI installer does not start

Check:

- the file is not blocked by Windows
- you have administrator permission
- the installer came from the official/internal project source
- the file extension is `.msi`

### Login fails

Check:

- username spelling
- password spelling
- whether the account is still active
- whether the VPN portal is online

## 5. Where to Put the Certificate and Installer

Recommended GitHub-safe structure:

```text
NeuChessHu-web/
  docs/
    vpn/
      VPN_INSTALLATION_GUIDE.md
  tools/
    vpn/
      README.md
      RootCA.crt
```

### Certificate

You may commit `RootCA.crt` only if:

- it contains only the public certificate
- it does not contain a private key
- it is intended to be distributed to users

Never commit:

- `.key`
- `.pfx`
- `.p12`
- certificate passwords
- VPN user passwords

### GlobalProtect MSI

Do not commit the GlobalProtect MSI directly unless your team is allowed to redistribute it.

Better options:

- upload it to a private GitHub Release
- store it in private cloud storage
- store it in an internal school/company file share
- use Git LFS only if the repo owner approves it

In the repository, keep only a reference file:

```text
tools/vpn/README.md
```

Example:

```md
# VPN Installer Files

Download the GlobalProtect MSI from the internal shared folder:

- Login datas and vpn are stored: [Drive](https://drive.google.com/file/d/1M0sNi8yjEA8oecccynu80Jq2pt9xj0Q0/view?usp=sharing)

Required files:

- RootCA.crt
- GlobalProtect64.msi
```

## 6. Security Notes

- The RootCA certificate allows Windows to trust certificates issued by that CA. Only install it if it comes from the project administrator.
- The GlobalProtect installer should show **Verified publisher: Palo Alto Networks, Inc**.
- Do not publish VPN credentials in GitHub.
- If the GitHub repository is public, do not upload the MSI or sensitive VPN details.
