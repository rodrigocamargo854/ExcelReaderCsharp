import styled from 'styled-components';

export const Report= styled.body`
    text-align: center;
`


export const ReportHeader= styled.header`
    background-color: #282c34;
    min-height: 60vh;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    font-size: calc(10px + 2vmin);
    color: white;
`



export const ReportBody = styled.div`
    height: 10vh;
    width: 100%;
    display: flex;
    justify-content: center;
    align-items: center;
`

export const P = styled.p`
margin-top: 13px;
margin-bottom: 0rem;
` 




export const ButtonHover = styled.button`
    &:hover {
    background-color: #61dafb;
    color: #fff;
    cursor: pointer;
    }
`

export const H3 = styled.h3`
    text-align: center;
    margin: 21px;
    line-height: 1;
    padding-bottom: 20px;
`
