import React from 'react';
import { Spinner } from "react-bootstrap";

import pdfMake from "pdfmake/build/pdfmake";
import pdfFonts from "pdfmake/build/vfs_fonts";
import { data } from './data';
import { Impressao } from './impressao.js';
import {Report,H3,ReportHeader,ReportBody,P,ButtonHover} from'./Reports';

pdfMake.vfs = pdfFonts.pdfMake.vfs;


function Reports() {

    const visualizarImpressao = async () => {
        console.log('report', data);
        const classeImpressao = new Impressao(data);
        const documento = await classeImpressao.PreparaDocumento();
        pdfMake.createPdf(documento).open({}, window.open('', '_blank'));
    }

    return (
        <Report>
            <H3>Relatório de produtos pesquisados</H3>
            <ReportHeader>
                <Spinner animation="border" role="status">
                    <span className="sr-only">Loading...</span>
                </Spinner>
                <P>
                    Criando Relatorio de pesquisa por produtos
          </P>
            </ReportHeader>
            <ReportBody>
                <ButtonHover onClick={visualizarImpressao}>
                    Visualizar documento
          </ButtonHover>
            </ReportBody>
        </Report>
    );
}

export default Reports;