import React, { useState } from "react";
import { Button } from "../../UI/Button/index";
import {
  NavAside,
  NavAsideContent,
  InputPrice,
  Label,
  SelectOrdering,
} from "./styled";

const MenuAside = (props) => {
  const [priceMin, setPriceMin] = useState(0);
  const [priceMax, setPriceMax] = useState(99999);
  const [ordering, setOrdering] = useState(" ");

  const handlerPrices = (e) => {
    e.preventDefault();
    props.onChangeFilters(priceMin, priceMax, ordering);
  };

  return (
    <NavAside>
      <NavAsideContent>
        <form onSubmit={handlerPrices}>
          <Label style={{ margin: "10px 5px 3px 2px" }}>Price min</Label>
          <InputPrice
            style={{ marginLeft: "9px" }}
            type="number"
            value={priceMin}
            onChange={(e) => setPriceMin(e.target.value)}
          />
          <Label style={{ margin: "10px 5px 3px 2px" }}>Price max</Label>
          <InputPrice
            type="number"
            value={priceMax}
            onChange={(e) => setPriceMax(e.target.value)}
          />
          <Label style={{marginTop: '10px'}}>Ordernar por:</Label>
          <SelectOrdering name="order" value={ordering} onChange={(e) => setOrdering(e.target.value)}>
            <option></option>
            <option value='max'>Maior Preço</option>
            <option value='min'>Menor Preço</option>
            <option value='az'>A-Z</option>
            <option value='za'>Z-A</option>
          </SelectOrdering>

          <Button style={{ margin: "10px" }}>Filtrar</Button>
        </form>
      </NavAsideContent>
    </NavAside>
  );
};

export default MenuAside;
