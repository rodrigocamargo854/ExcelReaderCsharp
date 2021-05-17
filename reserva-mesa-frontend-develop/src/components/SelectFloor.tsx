import { useContext, useEffect, useState } from "react";
import { Select } from "antd";

import { Floor } from "~/domain/models/floor";
import * as floorService from "~/services/http/backend/floor-service";

import { UserMapContext } from "~/contexts/UserMapContext";
import { SelectValue } from "antd/lib/select";

const SelectFloor: React.FC = () => {
  const { setFloorId, unitId, setFloorCode } = useContext(UserMapContext);

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
    const floorId = parseInt(event.toString());
    const floor = floors.find((floor) => floor.id == floorId);
    if (floor) {
      setFloorId(floorId);
      setFloorCode(floor.code);
    }
  };

  return (
    <div className="select-floor">
      <span>Andar</span>
      <Select style={{ width: "100%" }} onChange={handleChange}>
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
