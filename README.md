# azure-ad-b2c-sample

## summary

- To make sure what can I do with [Azure AD B2C](https://docs.microsoft.com/ja-jp/azure/active-directory-b2c/)
- Basicaly referring to the tutorial prepared
  - [Solutions and Training for Azure Active Directory B2C @ MSDocs](https://docs.microsoft.com/ja-jp/azure/active-directory-b2c/solution-articles)
    - [Gaining Expertise in Azure AD B2C: A Course for Developers](https://aka.ms/learnAADB2C)
- I will leave the information used for verification in this repository

## References

### Repositories

- [active-directory-b2c-training-course](https://github.com/Azure-Samples/active-directory-b2c-training-course)
  - In Module 3 (Application Integration for a .Net Web App)
    - The corresponding MSDocs is the page: [Tutorial: Enable authentication in a web application using Azure Active Directory B2C](https://docs.microsoft.com/ja-jp/azure/active-directory-b2c/active-directory-b2c-tutorials-web-app)
      - But the sample web app is different from the tutorial, so be careful
    - In step 7, could not be display my token contents
      - The token may not have been successfully delivered to https://jwt.ms

- [active-directory-b2c-custom-policy-starterpack](https://github.com/Azure-Samples/active-directory-b2c-custom-policy-starterpack)
  - In Module 5 (Introduction to Custom Policies)
    - The corresponding MSDocs is the page: [Get started with custom policies in Azure Active Directory B2C](https://docs.microsoft.com/ja-jp/azure/active-directory-b2c/active-directory-b2c-get-started-custom#register-identity-experience-framework-applications)
  - In Module 6 (Custom Policies 2 – REST APIs)
    - The corresponding MSDocs is the page: [Integrate REST API claims exchanges in your Azure AD B2C user journey as validation of user input](https://docs.microsoft.com/ja-jp/azure/active-directory-b2c/active-directory-b2c-custom-rest-api-netfw)
      - But, the sample web app is different from the tutorial, so be careful
      - In the sub-section "Update Custom Policies"
        of the section "Modify the Profile Edit Policy to Return the User Store Membership Date",
        in step 3), need to add the following *Item* element in the *Metadata* element

            <Item Key="AllowInsecureAuthInProduction">true</Item>

        - The sample of the page ([Step 5: Add a claims provider](https://docs.microsoft.com/ja-jp/azure/active-directory-b2c/active-directory-b2c-custom-rest-api-netfw#step-5-add-a-claims-provider)) is helpful
  - In Module 7 (External IDPs, OIDC and SAML)
    - The section "Integrate an OIDC IDP and Map Claims"
      - The corresponding MSDocs is the page: [Set up sign-in with a Google account using custom policies in Azure Active Directory B2C](https://docs.microsoft.com/ja-jp/azure/active-directory-b2c/active-directory-b2c-custom-setup-goog-idp)
    - The section "Integrate a SAML IDP and Map Claims"
      - The corresponding MSDocs is the page: [Set up sign-in with a Salesforce SAML provider by using custom policies in Azure Active Directory B2C](https://docs.microsoft.com/ja-jp/azure/active-directory-b2c/active-directory-b2c-setup-sf-app-custom)
        - You should be careful with the setting of a connected app in Salesforce.
          In the *Entity ID* and the *ACS URL* field, you must enter the URL in lower case.
  - In Module 9 (Auditing and Reporting)
    - The corresponding MSDocs is the page: [Azure Active Directory B2C: Collecting Logs](https://docs.microsoft.com/ja-jp/azure/active-directory-b2c/active-directory-b2c-troubleshoot-custom)
      - You should be careful with the note in the above page

        > Do not use development mode in production.
        > Logs collect all claims sent to and from the identity providers during development.
        > ...
        > These detailed logs are only collected when the policy is placed on DEVELOPMENT MODE.

      - In the section "Use Audit logs to Export Application, User Flow, and User Activity Data ",
        Using the Extensions file (for example, *TrustFrameWorkExtensions.xml*) did not work well.
        So I used the Relying Party (RP) file (for example, *SignUpOrSignin.xml*).
        - The sample of the page ([Set up the custom policy](https://docs.microsoft.com/ja-jp/azure/active-directory-b2c/active-directory-b2c-troubleshoot-custom#set-up-the-custom-policy)) is helpful

### Points

- Cross-origin resource sharing(CORS) for customizing UI
  - [Enable CORS](https://docs.microsoft.com/ja-jp/azure/active-directory-b2c/tutorial-customize-ui#enable-cors)
  - In step 2, for Allowed origins
    > "You need to use all lowercase letters when entering your tenant name."

- Signing certificate for SAML requests
  - [Generate a signing certificate](https://docs.microsoft.com/ja-jp/azure/active-directory-b2c/active-directory-b2c-setup-sf-app-custom#generate-a-signing-certificate)
  - If you need to generate a signing certificate, the following commands for PowerShell will be helpful.
    But, it will expire in a year, so be careful.

        # Make sure that you update the following two lines.
        $tenantName = "<YOUR TENANT NAME>.onmicrosoft.com"
        $pwdText = "<YOUR PASSWORD HERE>"

        $Cert = New-SelfSignedCertificate -CertStoreLocation Cert:\CurrentUser\My -DnsName "SamlIdp.$tenantName" -Subject "B2C SAML Signing Cert" -HashAlgorithm SHA256 -KeySpec Signature -KeyLength 2048
        $pwd = ConvertTo-SecureString -String $pwdText -Force -AsPlainText

        Export-PfxCertificate -Cert $Cert -FilePath .\B2CSigningCert.pfx -Password $pwd
