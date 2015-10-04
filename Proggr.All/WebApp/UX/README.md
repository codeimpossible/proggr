# proggr UX App

The UX section of the codebase is all the code for our frontend. The baseline css (bootstrap, font-awesome) is kept in the visual studio project (in the [Content folder](./../Content)). All of the css/js specific to the react application is in this directory.

## Built on React

Like I mentioned above, this is built on react. I'll probably pull in a flux-like system for handling propagation of data through the app.

## Running in development

The main .net web application is set to load the ux application from a bundle. This bundle defaults to a CDN  - which is set to the address of the webpack dev server configured in the gulpfile. If that CDN isn't available (when in production) the server will expect the file ux application code to have been webpack'd to `/UX/dist/app-bundle.js`.

**TL;DR** So during development you'll have to run `gulp webpack:server` to make sure that the react app is available to the .net side. In production the react app will be webpack'd into a single js and included that way.

## Webpack Only

The nice thing about the gulp build for this project is that everything is funneled through webpack. All `*.jsx`, `*.css`, `*.es6` files are run through the appropriate loaders when webpack is invoked; We don't need a separate task to handle them.
