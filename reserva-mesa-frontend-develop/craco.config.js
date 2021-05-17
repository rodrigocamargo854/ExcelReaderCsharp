const CracoLessPlugin = require("craco-less");

var baseUrl = "src/";
module.exports = {
  eslint: {
    enable: false
  },
  webpack: {
    alias: {
      assets: `${baseUrl}/assets/`,
      '~': `${baseUrl}/`
    }
  },
  jest: {
    configure: {
      moduleNameMapper: {
        // Jest module mapper which will detect our absolute imports.
        '^assets(.*)$': '<rootDir>/src/assets$1',
        '^components(.*)$': '<rootDir>/src/components$1',
        '^interfaces(.*)$': '<rootDir>/src/interfaces$1',
        '^modules(.*)$': '<rootDir>/src/modules$1',
        '^utils(.*)$': '<rootDir>/src/utils$1',

        // Another example for using a wildcard character
        '^~(.*)$': '<rootDir>/src$1'
      }
    }
  },
  plugins: [
    {
      plugin: CracoLessPlugin,
      options: {
        lessLoaderOptions: {
          lessOptions: {
            javascriptEnabled: true,
          },
        },
      },
    },
  ],
};
