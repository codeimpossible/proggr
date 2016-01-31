var path = require('path');
var webpack = require('webpack');

module.exports = {
  entry: [path.resolve(__dirname, './src/app.es6')],
  output: {
    path: path.resolve(__dirname, './dist'),
    filename: 'app-bundle.js',
    publicPath: 'http://localhost:9090/webpack/'
  },
  plugins: [
    new webpack.HotModuleReplacementPlugin(),
    new webpack.NoErrorsPlugin() // don't reload if there is an error
  ],
  resolve: {
    extensions: ['', '.webpack.js', '.web.js', '.js', '.jsx', '.es6']
  },
  module: {
    loaders: [
      {
        test: /\.js$/,
        exclude: /node_modules/,
        loaders: ['react-hot', 'babel-loader'],
        include: path.join(__dirname, 'src')
      },
      {
        test: /\.jsx$/,
        exclude: /node_modules/,
        loaders: ['react-hot', 'babel-loader']
      },
      {
        test: /\.es6$/,
        exclude: /node_modules/,
        loaders: ['react-hot', 'babel-loader']
      },
      // Extract css files
      {
        test: /\.css$/,
        loader: 'style-loader!css-loader'
      },
      // Optionally extract less files
      // or any other compile-to-css language
      {
        test: /\.less$/,
        loader: 'style-loader!css-loader!less-loader'
      }
      // You could also use other loaders the same way. I. e. the autoprefixer-loader
    ]
  }
};