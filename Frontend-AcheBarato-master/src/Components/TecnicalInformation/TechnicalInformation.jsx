import { Card, Table, Container } from "react-bootstrap";

const TechnicalInformation = (props) => {
  
  return (
    <Card>
      <Card.Header>
        <Card.Title>Ficha Técnica</Card.Title>
      </Card.Header>
      <Card.Body>
        <Container>
          <Table striped bordered>
            <tbody>{props.productInformations.map((data,index) =>(
              <tr key={index}>
                <th>{data.name}</th>
                <td>{data.value}</td>
              </tr>
            ))}</tbody>
          </Table>
        </Container>
      </Card.Body>
    </Card>
  );
};
export default TechnicalInformation;
