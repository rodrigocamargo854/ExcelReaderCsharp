import React, { useContext } from 'react';
import { MapLegend, SelectFloor, SelectUnit } from '~/components';

import Map from '~/components/Map'
import { UserMapContext } from '~/contexts/UserMapContext';
import floors from "~/assets/floor-maps/fullscreen/floors";
import { Button } from 'antd';
import { ExpandOutlined } from '@ant-design/icons'
import { FullScreen, useFullScreenHandle } from "react-full-screen";

const FullscreenMap = () => {
  const { setFinalDate, setInitialDate } = useContext(
    UserMapContext
  );

  let newDate = new Date()
  let date = newDate.getDate();
  let month = newDate.getMonth() + 1;
  let year = newDate.getFullYear();
  let formatedDate = year + "/" + month + "/" + date;

  setInitialDate(formatedDate);
  setFinalDate(formatedDate);

  const handle = useFullScreenHandle();

  return (
    <>
      <div className="selects">
        <SelectUnit />
        <SelectFloor />
      </div>
      <MapLegend />
      <FullScreen handle={handle}>
        <Map floors={floors}/>
      </FullScreen>
      <Button className="fullscreen-next-btn" onClick={handle.enter}><ExpandOutlined /></Button>
    </>
  );
}

export default FullscreenMap;
