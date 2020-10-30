var path = require("path");

module.exports = {
  entry: ["./index.jsx"],
  output: {
    path: path.resolve(__dirname, "./public"),
    filename: "scripts.js",
  },
  devServer: {
    contentBase: "public",
    compress: true,
    publicPath: "/",
    port: 9000,
  },
  module:{
      rules:[
          {
              test: /\.jsx?$/,
              exclude: /(node_modules)/,
              loader: "babel-loader",
              options:{
                  presets:["@babel/preset-env", "@babel/preset-react"]
              }
          }
      ]
  },
};
