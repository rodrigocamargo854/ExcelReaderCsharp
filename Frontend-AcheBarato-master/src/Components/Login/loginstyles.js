import styled from "styled-components";
import { Button, Jumbotron } from "react-bootstrap";



export const Form = styled.form`
  width: 400px;
  background: #fff;
  padding: 20px;
  display: flex;
  flex-direction: column;
  align-items: center;
  img {
    width: 300px;
    margin: 10px 0 40px;
  }
  p {
    font-size:2rem;
    color: #719ECE;
    margin-bottom: 0;
    padding: 10px;
    text-align: center;
  }
  input {
    display: block;
    margin-bottom: 2rem;
    display: flex;
    display: block;
    width: 70%;
    height: calc(1.5em + .75rem + 1px);
    font-size: 1rem;
    font-weight: 400;
    line-height: 1.5;
    color: #495057;
    background-color: #fff;
    background-clip: padding-box;
    border: 1px solid #f2f5f8;
    border-color: #719ECE;
    border-radius: 0.5rem;
    transition: border-color .15s ease-in-out, box-shadow .15s ease-in-out;

    }
  
 
  hr {
    margin: 20px 0;
    border: none;
    border-bottom: 1px solid #cdcdcd;
    width: 100%;
  }
  a {
    font-size: 16;
    font-weight: bold;
    color: #999;
    text-decoration: none;
  }
`;

export const FormPage = styled.main`
height: 100%;
display: flex;
justify-content: center;
align-items: center;
`

export const LoginButton = styled(Button)`
    width: 30%;
    height: 2.4rem;
    border: none;
    margin-left:1rem;
    cursor: pointer;
    background: #13d0f1;
    border-radius: 0.5rem;
    color: #ffffff;
    display: flex;
    align-items: center;
    justify-content: center;
    display: block;
    
    `;

export const LoginPage = styled.main`
    height: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
`

export const JumbotronStyled = styled(Jumbotron)`
flex-direction:column;
display: flex;
justify-content: center;
align-items: center;
width:fit-content;
background-color: #ffff;
border-radius: 2rem;
color: #1a1515;
justify-content: center;
opacity: 100%;
box-shadow: inset -1px 4px 27px 7px #ffff, 0px 3px 13px -5px black
 `;