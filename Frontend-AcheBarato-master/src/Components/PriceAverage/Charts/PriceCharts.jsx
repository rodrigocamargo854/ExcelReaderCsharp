import * as React from "react";
import "./PriceCharts.css";
import ReactLoading from "react-loading";
import { Chart } from "react-google-charts";
import { Container, Row, Col } from "react-bootstrap";
import AlertPrice from "../../AlertPrice/AlertPrice";
import AlertPopup from "../../AlertPopUp/index";

const PriceCharts = (props) => {
  const dataArray = [["Variação de preço", "Variação de Preços"]];
  const historicalPrice = props.data.historycalṔrices;
  const output = historicalPrice.map(({ dateOfPrice, priceOfThatDay }) =>
    dataArray.push([dateOfPrice, priceOfThatDay])
  );
  const [showPop, setShowPop] = React.useState(0);

  console.log(showPop);
  return (
    <Container fluid>
      <Row>
        <Col>
          <Chart
            chartType="ColumnChart"
            spreadSheetUrl="https://docs.google.com/spreadsheets/d/1jN0iw0usssnsG1_oi-NXtuKfsUsGme09GsFidbqxFYA"
            toolbarItems={[
              {
                type: "csv",
                datasource:
                  "https://spreadsheets.google.com/tq?key=1jN0iw0usssnsG1_oi-NXtuKfsUsGme09GsFidbqxFYA",
              },
            ]}
            rootProps={{ "data-testid": "1" }}
            width={400}
            height={"300px"}
            chartType="AreaChart"
            loader={
              <ReactLoading
                type="spokes"
                color="#ff6633"
                height={67}
                width={37}
              />
            }
            data={dataArray}
            options={{
              title: "Historico de Preços",
              hAxis: { title: "Datas", titleTextStyle: { color: "#0b34eb" } },
              vAxis: { minValue: 0 },

              // For the legend to fit, we make the chart area smaller
              chartArea: { width: "50%", height: "70%" },
              // lineWidth: 25
            }}
          />
        </Col>
        <Col>
          <AlertPrice show={setShowPop} product={props.data} />
          {showPop === 204 ? (
            <div style={{ marginTop: "20px" }}>
              <AlertPopup text={"Seu alarme foi salvo!!"} />
            </div>
          ) : (
            <span></span>
          )}
          {showPop === -1 ? (
            <div style={{ marginTop: "20px" }}>
              <AlertPopup text={"Você precisa estar logado!!"} />
            </div>
          ) : (
            <span></span>
          )}
        </Col>
      </Row>
    </Container>
  );
};

export default PriceCharts;
