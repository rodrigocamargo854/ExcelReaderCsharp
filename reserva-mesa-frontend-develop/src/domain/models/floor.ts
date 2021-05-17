interface Floor {
  id: number;
  name: string;
  active: boolean;
  unitId: number;
  code: string;
}

interface UpdateFloor {
  name: string;
  active: boolean;
  unityId: number;
}

export type { Floor, UpdateFloor }
