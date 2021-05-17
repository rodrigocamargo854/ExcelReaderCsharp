import { useContext, useEffect, useState } from "react";

import { UserRectStatus } from "../enums";
import { CreateReservationContext } from "~/contexts/CreateReservationContext";
import { UserMapContext } from "~/contexts/UserMapContext";
import * as workstationService from "~/services/http/backend/workstation-service";
import { Workstation } from "~/domain/models/workstation";

function useUserMap(workstationsMap: Map<string, UserRectStatus>) {
  const { floorId, initialDate, finalDate } = useContext(UserMapContext);
  const { modalIsOpen } = useContext(CreateReservationContext);
  const [workstations, setWorkstations] = useState<Map<string, UserRectStatus>>(
    workstationsMap
  );
  const [loading, setLoading] = useState<boolean>(false);

  async function setDisabledWorkstations() {
    const disabledWorkstationResponse = await workstationService.getDisabledWorkstations(
      floorId
    );

    if (disabledWorkstationResponse.statusCode !== 200) return;

    disabledWorkstationResponse.data.map(
      (disabledWorkstation: Workstation): void => {
        workstations.set(disabledWorkstation.name, UserRectStatus.disabled);
      }
    );

    setWorkstations(workstations);
  }

  async function setReservedWorkstations() {
    if (finalDate !== undefined) {
      const reservedWorkstationsResponse = await workstationService.getReservedWorkstationByFloorIdAndDateInterval(
        floorId,
        initialDate,
        finalDate
      );
      if (reservedWorkstationsResponse.statusCode !== 200) return;

      reservedWorkstationsResponse.data.map((reservedWorkstationName): void => {
        workstations.set(reservedWorkstationName, UserRectStatus.reserved);
      });

      setWorkstations(workstations);
    }
  }

  function resetWorkstationsStatus() {
    workstations.forEach(
      (value, key, map): Map<string, UserRectStatus> =>
        map.set(key, UserRectStatus.available)
    );
    setWorkstations(workstations);
  }

  useEffect(() => {
    (async () => {
      setLoading(true);
      resetWorkstationsStatus();
      await setReservedWorkstations();
      await setDisabledWorkstations();
      setLoading(false);
    })();
  }, [floorId, initialDate, finalDate, modalIsOpen]);

  return { loading, workstations };
}

export default useUserMap;
