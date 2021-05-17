import React, { useContext } from "react";
import { AdminMapContext } from "~/contexts";

import {
  Map,
  MapLegend,
  UpdateWorkstationModal,
  SelectFloor,
  SelectUnit,
} from "./components";

const AdminMap = () => {
  const { floorId, unitId } = useContext(AdminMapContext);

  return (
    <>
      <UpdateWorkstationModal />
      <div className="admin-map">
        <header>
          <SelectUnit />
          <SelectFloor />
        </header>
      </div>
      {unitId && floorId ? (
        <>
          <MapLegend />
          <Map />
        </>
      ) : (
        <></>
      )}
    </>
  );
};

export default AdminMap;
