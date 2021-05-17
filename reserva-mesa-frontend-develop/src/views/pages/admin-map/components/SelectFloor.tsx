import { useContext, useEffect, useState } from "react";
import { Select } from "antd";

import { Floor } from "~/domain/models/floor";
import * as floorService from "~/services/http/backend/floor-service";

import { AdminMapContext } from "~/contexts";
import { SelectValue } from "antd/lib/select";

const SelectFloor = () => {
  const { setFloorId, unitId } = useContext(AdminMapContext);

  const [floors, setFloors] = useState<Floor[]>([]);

  useEffect(() => {
    (async () => {
      if (unitId) {
        const responseData = (await floorService.getFloorsByUnitId(unitId))
          .data;

        setFloors(responseData.filter((floor) => floor.active));
      }
    })();
  }, [unitId]);

  const handleChange = (event: SelectValue): void => {
    setFloorId(parseInt(event.toString()));
  };

  return (
    <div className="select-floor">
      <span>Andar</span>
      <Select onChange={handleChange}>
        {floors.map((floor) => (
          <Select.Option value={floor.id} key={floor.id}>
            {floor.name}
          </Select.Option>
        ))}
      </Select>
    </div>
  );
};

export default SelectFloor;
