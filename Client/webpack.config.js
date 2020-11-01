const path = require("path");

module.exports = {
  entry: ["./index.tsx"],
  devtool: 'inline-source-map',
  output: {
    path: path.resolve(__dirname, "public"),
    filename: "index.js",
  },
  module: {
    rules: [
      {
        test: /\.tsx?$/,
        use: 'ts-loader',
        exclude: /node_modules/,
      },
      {
        test: /\.css$/i,
        use: ['style-loader', 'css-loader'],
      }
    ],
  },
  resolve: {
    extensions: [ '.tsx', '.ts', '.js' ],
  },
  devServer: {
    contentBase: "public",
    compress: true,
    publicPath: "/",
    port: 9000,
  },
};
