import React, { useContext, useEffect, useState } from "react";
import { AdminMapContext } from "~/contexts";

import floors from "~/assets/floor-maps/admin/floors/index";
import { getFloorById } from "~/services/http/backend/floor-service";

const Map = () => {
  const { floorId } = useContext(AdminMapContext);

  const [floorCode, setFloorCode] = useState("");

  useEffect(() => {
    (async () => {
      const response = await getFloorById(floorId);
      setFloorCode(response.data.code);
    })();
  }, [floorId]);

  return (
    <div className="map">
      {floors.map((floor) => floor.code === floorCode && <floor.map />)}
    </div>
  );
}

export default Map;
