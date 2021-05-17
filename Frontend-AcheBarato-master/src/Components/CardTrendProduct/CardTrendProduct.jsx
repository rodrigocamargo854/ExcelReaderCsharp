import { Card } from "react-bootstrap";
import { Button } from "../../UI/Button/index";
import { Link } from "react-router-dom";
import styled from "styled-components";

const CardStyled = styled(Card)`
  width: 14rem;
  max-width: 14rem;
  max-height: 280px;
  margin: 10px;
  font-size: 10px;
`;
const CardTitleStyled = styled(Card.Title)`
  font-size: 10px;
`;

const CardTrendProduct = (props) => {
  return (
    <>
      <CardStyled>
        <Card.Img
          style={{ width: 100, height: 100, margin:'10px auto'}}
          variant="top"
          src={props.productThumbImage}
        />
        <Card.Body>
          <CardTitleStyled style={{ fontSize: 10 }}>
            {props.productName}
          </CardTitleStyled>
          <CardStyled.Text>
            <strong>R$ {props.productPrice}</strong>
          </CardStyled.Text>
          <Link to={`/ProdutoEscolhido/${props.productId}`}>
            <Button style={{ margin: "auto" }}>Ver Produto</Button>
          </Link>
        </Card.Body>
      </CardStyled>
    </>
  );
};

export default CardTrendProduct;
