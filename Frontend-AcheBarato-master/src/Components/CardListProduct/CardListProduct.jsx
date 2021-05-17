import { Card, Row, Col } from "react-bootstrap";
import { Link, BrowserRouter } from "react-router-dom";
import { Button } from "../../UI/Button/index";


const CardListProduct = (props) => {
  return (
    <Card className="mb-3">
      <Card.Body>
        <Row>
          <Col xs={3}>
            <Card.Img src={props.productImg} style={{ width: '90px', marginLeft:'100px' }} />
          </Col>
          <Col xs={3} className="justify-content-start">
            <BrowserRouter forceRefresh={true}>
              <Link to={`/ProdutoEscolhido/${props.productId}`}>
                <p>{props.productName}</p>
              </Link>
            </BrowserRouter>
          </Col>
          <Col xs={3} className="justify-content-center ml-auto">
            <h4>R$ {parseFloat(props.productPrice)}</h4>
          </Col>
          <Col xs={3}>
            <a href={props.productLinkRedirec}>
              <Button variant="primary">Link para Loja</Button>
            </a>
          </Col>
        </Row>
      </Card.Body>
    </Card>
  );
};

export default CardListProduct;
