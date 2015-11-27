# train-ticket-booking-system
University project.

# How to run it locally

Go to the root of the MVC project and make a file called ```ApiCredentials.config``` with the following content:

```c#
<appSettings>
    <add key="SENDGRID_USERNAME" value="your_username" />
    <add key="SENDGRID_PASSWORD" value="your_password" />
</appSettings>
```

You also need to run ```bower install``` to download and resolve the front-end dependencies.
