import { createGlobalStyle } from 'styled-components'

export const GlobalStyles = createGlobalStyle `
  html, body {
    width: 100vw;
    height: 100vh;
    padding: 0;
    margin: 0 ;
    
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    background-color:  orange;
  }

  #root {
    height: 100%;
}

*.p.h1.h3{
    font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen', 'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue', sans-serif;
}

code {
    font-family: source-code-pro, Menlo, Monaco, Consolas, 'Courier New', monospace;
}
`