# azure-ad-b2c-sample

## summary

- to make sure what can I do with [Azure AD B2C](https://docs.microsoft.com/ja-jp/azure/active-directory-b2c/)
- basicaly referring to the tutorial prepared
  - [Solutions and Training for Azure Active Directory B2C @ MSDocs](https://docs.microsoft.com/ja-jp/azure/active-directory-b2c/solution-articles)
    - [Gaining Expertise in Azure AD B2C: A Course for Developers](https://aka.ms/learnAADB2C)
- leave the information used for verification in this repository

## References

### Repositories

- [active-directory-b2c-training-course](https://github.com/Azure-Samples/active-directory-b2c-training-course)
  - in Module 3 (Application Integration for a .Net Web App)
    - the corresponding MSDocs is the page: [Tutorial: Enable authentication in a web application using Azure Active Directory B2C](https://docs.microsoft.com/ja-jp/azure/active-directory-b2c/active-directory-b2c-tutorials-web-app)
      - but the sample web app is different from the tutorial, so be careful
    - in step 7, could not be display my token contents
      - the token may not have been successfully delivered to https://jwt.ms

- [active-directory-b2c-custom-policy-starterpack](https://github.com/Azure-Samples/active-directory-b2c-custom-policy-starterpack)
  - in Module 5 (Introduction to Custom Policies)
    - the corresponding MSDocs is the page: [Get started with custom policies in Azure Active Directory B2C](https://docs.microsoft.com/ja-jp/azure/active-directory-b2c/active-directory-b2c-get-started-custom#register-identity-experience-framework-applications)
  - in Module 6 (Custom Policies 2 – REST APIs)
    - the corresponding MSDocs is the page: [Integrate REST API claims exchanges in your Azure AD B2C user journey as validation of user input](https://docs.microsoft.com/ja-jp/azure/active-directory-b2c/active-directory-b2c-custom-rest-api-netfw)
      - but the sample web app is different from the tutorial, so be careful
      - in the sub-section "Update Custom Policies"
        of the section "Modify the Profile Edit Policy to Return the User Store Membership Date",
        in step 3), metadata key "AllowInsecureAuthInProduction" (set to true) is missing

        `<Item Key="AllowInsecureAuthInProduction">true</Item>`
        - the sample in MSDocs is helpful

### Points

- Cross-origin resource sharing(CORS) for customizing UI
  - [Enable CORS](https://docs.microsoft.com/ja-jp/azure/active-directory-b2c/tutorial-customize-ui#enable-cors)
  - in step 2, for Allowed origins
    - "You need to use all lowercase letters when entering your tenant name."

