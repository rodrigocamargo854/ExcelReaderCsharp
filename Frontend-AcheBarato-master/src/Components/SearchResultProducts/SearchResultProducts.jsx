import { ListGroup } from "react-bootstrap";
import styled from "styled-components";
import CardTrendProduct from "../CardTrendProduct/CardTrendProduct";

const CardSearchResult = styled(CardTrendProduct)`
  border: none;
  height: 10rem;
`;

const ListCard = styled(ListGroup.Item)`
  border: none;
`;

const SearchResultProducts = (props) => {
  return (
    <ListCard>
      <CardSearchResult
        productName={props.productName}
        productPrice={props.productPrice}
        productThumbImage={props.thumbImgLink}
      />
    </ListCard>
  );
};

export default SearchResultProducts;
