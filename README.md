# Proggr

Is a open-source software developer analytics website built on top of the Asp.net MVC framework. Just add your github account and watch Proggr tell you interesting things!

## License

This project is licensed under the [BSD 3-clause license](./LICENSE). For more information, [read about the BSD 3-clause license on choosealicense.org](http://choosealicense.com/licenses/bsd-3-clause/).

## Getting Started

You'll need the following to run this project:

 - Visual Studio 2015 of some kind (perhaps Community Edition if you don't have Pro or up)
 - nodejs & npm (at least v0.12.7... v4.x might work too)
 - SQL Server Express 2014
 - a github account

Create a `proggr` folder at `C:\proggr`. Add a new file there named `.proggrrc`. Go to github, create a new application named "proggr_dev" or "proggr" and add the client secret and client id codes you get to your `.proggrrc` file, like in the example below.

```json
{
  "ClientId": "YOUR_GITHUB_APP_CLIENT_ID",
  "ClientSecret": "YOUR_GITHUB_APP_CLIENT_SECRET"
}
```

Now install [Sql Server Express 2014](http://www.hanselman.com/blog/DownloadSQLServerExpress.aspx) and run the [Create Database SQL Script](./Data/CreateDatabase.sql). This will create the proggr database for you. You'll need to change the `"DefaultConnection"` connection string if your sql express instance isn't the default.

Now go into the `./WebApp/UX` folder and run `npm install`. Now you should be ready to run the project!

## Running the code

Open up a terminal (powershell is recommended). Go to the `./WebApp/UX` directory and run `gulp webpack:server`. Now open the `./Proggr.All/Proggr.All.sln` in Visual Studio and run it!
