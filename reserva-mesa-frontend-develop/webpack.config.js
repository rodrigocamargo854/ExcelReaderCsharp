const dotenv = require('dotenv');
const webpack = require('webpack');

// webpack.config.js
module.exports = () => {
  const env = dotenv.config().parsed;
  
  const envKeys = Object.keys(env).reduce((prev, next) => {
    prev[`process.env.${next}`] = JSON.stringify(env[next]);
    return prev;
  }, {});

  return {
    plugins: [
      new webpack.DefinePlugin(envKeys)
    ],
    rules: [{
      test: /.less$/,
      use: [{
        loader: 'style-loader',
      }, {
        loader: 'css-loader', // translates CSS into CommonJS
      }, {
        loader: 'less-loader', // compiles Less to CSS
       options: {
         lessOptions: { // If you are using less-loader@5 please spread the lessOptions to options directly
           javascriptEnabled: true,
         },
       },
      }],
    }],
  }
}