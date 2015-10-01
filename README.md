# Proggr

Is a open-source software developer analytics website built on top of the Asp.net MVC framework. Just add your github account and watch Proggr tell you interesting things!

## Setting up

Create a `proggr` folder at `C:\proggr`. Add a new file there named `.proggrrc`. Go to github, create a new application named "proggr_dev" or "proggr" and add the client secret and client id codes you get to your `.proggrrc` file, like in the example below.

```json
{
  "ClientId": "YOUR_GITHUB_APP_CLIENT_ID",
  "ClientSecret": "YOUR_GITHUB_APP_CLIENT_SECRET"
}
```

Now install [Sql Server Express 2014](http://www.hanselman.com/blog/DownloadSQLServerExpress.aspx) and run the [Create Database SQL Script](./Data/CreateDatabase.sql). This will create the proggr database for you. You'll need to change the `"DefaultConnection"` connection string if your sql express instance isn't the default.
