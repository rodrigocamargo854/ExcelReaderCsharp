import { useContext, useEffect, useState } from "react";

import { FullscreenRectStatus } from "../enums";
import { CreateReservationContext } from "~/contexts/CreateReservationContext";
import { UserMapContext } from "~/contexts/UserMapContext";
import * as workstationService from "~/services/http/backend/workstation-service";
import { Workstation } from "~/domain/models/workstation";

function useFullscreenMap(workstationsMap: Map<string, FullscreenRectStatus>) {
  const { floorId, initialDate, finalDate } = useContext(UserMapContext);
  const { modalIsOpen } = useContext(CreateReservationContext);
  const [workstations, setWorkstations] = useState<
    Map<string, FullscreenRectStatus>
  >(workstationsMap);
  const [loading, setLoading] = useState<boolean>(false);

  async function setDisabledWorkstations() {
    const disabledWorkstationResponse = await workstationService.getDisabledWorkstations(
      floorId
    );

    if (disabledWorkstationResponse.statusCode !== 200) return;

    disabledWorkstationResponse.data.map(
      (disabledWorkstation: Workstation): void => {
        workstations.set(
          disabledWorkstation.name,
          FullscreenRectStatus.disabled
        );
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
        workstations.set(
          reservedWorkstationName,
          FullscreenRectStatus.reserved
        );
      });

      setWorkstations(workstations);
    }
  }

  function resetWorkstationsStatus() {
    workstations.forEach(
      (value, key, map): Map<string, FullscreenRectStatus> =>
        map.set(key, FullscreenRectStatus.available)
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

export default useFullscreenMap;
