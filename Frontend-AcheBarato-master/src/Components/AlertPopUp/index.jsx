import React, { useState } from "react";
import { Toast } from "react-bootstrap";
import styled from 'styled-components';

const AlertPopupBox = styled(Toast)`
 background-color: #ff6633;
`

const AlertPopup = ({ text }) => {
  const [showA, setShowA] = useState(true);
  const toggleShowA = () => setShowA(!showA);

  return (
    <AlertPopupBox show={showA} onClose={toggleShowA}>
      <Toast.Header>
        <img src="holder.js/20x20?text=%20" className="rounded mr-2" alt="" />
        <strong className="mr-auto">AcheBarato</strong>
      </Toast.Header>
      <Toast.Body style={{color:'#fff'}}>{text}</Toast.Body>
    </AlertPopupBox>
  );
};

export default AlertPopup;