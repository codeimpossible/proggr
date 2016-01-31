var gulp = require('gulp');
var path = require('path');
var gutil = require('gulp-util');
var webpack = require('webpack');
var WebpackDevServer = require('webpack-dev-server');
var pkg = require('./package.json');
var webpackConfig = require('./webpack.config');

var webpackResult = function(taskname, callback) {
  return function(err, stats) {
    if (err) throw new gutil.PluginError(taskname, err);
    gutil.log(taskname, stats.toString({
      colors: false
    }));
    callback();
  }
}

gulp.task('default', ['webpack:server'], function () { });
gulp.task('build', ['webpack:prod']);

// Build and watch cycle (another option for development)
// Advantage: No server required, can run app from filesystem
// Disadvantage: Requests are not blocked until bundle is available,
//               can serve an old app on refresh
gulp.task('dev', ['webpack:dev'], function () {
  gulp.watch(['src/**/*'], ['webpack:dev']);
});

// create a single instance of the compiler to allow caching
var devCompiler;
gulp.task('webpack:dev', function (callback) {
  webpackConfig.debug = true;
  devCompiler = devCompiler || webpack(webpackConfig);
  devCompiler.run(webpackResult('webpack:dev', callback));
});


gulp.task('webpack:prod', function (callback) {
  webpackConfig.plugins = webpackConfig.plugins.concat(
		new webpack.DefinePlugin({
		  "process.env": {
		    // This has effect on the react lib size
		    "NODE_ENV": JSON.stringify('production')
		  }
		}),
		new webpack.optimize.DedupePlugin(),
		new webpack.optimize.UglifyJsPlugin()
	);
  webpack(webpackConfig, webpackResult('webpack:prod', callback));
});


gulp.task('webpack:server', function (callback) {
  // modify some webpack config options
  var config = Object.create(webpackConfig);
  config.devtool = 'eval';
  config.debug = true;
  config.entry.unshift('webpack/hot/only-dev-server');
  config.entry.unshift('webpack-dev-server/client?http://localhost:9090');

  // Start a webpack-dev-server
  new WebpackDevServer(webpack(config), {
    publicPath: config.output.publicPath,
    path: path.resolve(__dirname, './dist'),
    filename: 'app-bundle.js',
    hot: true,
    inline: true,
    headers: { 'Access-Control-Allow-Origin': '*' },
    stats: {
      colors: false
    }
  }).listen(9090, '0.0.0.0', function (err) {
    if (err) throw new gutil.PluginError('webpack-dev-server', err);
    gutil.log('[webpack-dev-server]', 'http://localhost:9090/webpack/app-bundle.js');
  });
});
