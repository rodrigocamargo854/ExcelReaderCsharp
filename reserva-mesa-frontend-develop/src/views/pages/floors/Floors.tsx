import React, { useContext, useEffect, useState } from "react";

import { Divider, Select } from "antd";
import { SelectValue } from "antd/lib/select";

import { FloorsTable, UpdateFloorModal } from "./components";
import { Unit } from "~/domain/models/unit";
import { getUnits } from "~/services/http/backend/unit-service";
import { FloorsContext } from "~/contexts";

const Floors = () => {
  const { setUnitId } = useContext(FloorsContext);

  const [units, setUnits] = useState<Unit[]>([]);

  const handleChange = (event: SelectValue) => {
    setUnitId(parseInt(event.toString()));
  }

  useEffect(() => {
    (async () => {
      const response = await getUnits();
      setUnits(response.data);
    })();
  }, []);

  return (
    <>
      <UpdateFloorModal />
      <div className="floors">
        <div className="floors-select">
          <span>Unidade</span>
          <Select style={{ width: "100%" }} onChange={handleChange}>
            { units.map(unit => (
              <Select.Option value={unit.id} key={unit.id}>
                { unit.name }
              </Select.Option>
            )) }
          </Select>
        </div>
        <Divider />
        <FloorsTable />
      </div>
    </>
  );
}

export default Floors;
