# wordpress
Set up:
1. Ensure Docker containers are running
2. Ensure username is updated in the runsetttings file
3. Ensure the password is updated in the user secrets file
    {
      "EnvironmentDetails": {
        "LoginPassword": "password here"
       }
    }
4. Ensure the runsettints file is selected Test>Configure Run settings>Select solution wide runSettings file
5. Ensure playwright browsers are installed locally using bin/Debug/netX/playwright.ps1 install

Funtional Test:
The puropse of the funtional test is to ensure a user can create and update a page and asserts that the relevant updates have been made to the newly published page.  The test is set to run in Chromium but can easily be updated in the set up file to run using Webkit or Firefox