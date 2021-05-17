import React from 'react';
import { MapInteractionCSS } from 'react-map-interaction';

import Rect from '../UserRect';
import { UserRectStatus } from '../enums';
import useUserMap from '../hooks/useUserMap';

export default function SvgComponent(props: React.SVGProps<SVGSVGElement>) {
  const workstationsMap = new Map();
  workstationsMap.set('01-01-01-01', UserRectStatus.available);
  workstationsMap.set('01-01-01-02', UserRectStatus.available);
  workstationsMap.set('01-01-01-03', UserRectStatus.available);
  workstationsMap.set('01-01-01-04', UserRectStatus.available);
  workstationsMap.set('01-01-01-05', UserRectStatus.available);
  workstationsMap.set('01-01-01-06', UserRectStatus.available);
  workstationsMap.set('01-01-01-07', UserRectStatus.available);
  workstationsMap.set('01-01-01-08', UserRectStatus.available);
  workstationsMap.set('01-01-01-09', UserRectStatus.available);
  workstationsMap.set('01-01-01-10', UserRectStatus.available);
  workstationsMap.set('01-01-01-11', UserRectStatus.available);
  workstationsMap.set('01-01-01-12', UserRectStatus.available);
  workstationsMap.set('01-01-01-13', UserRectStatus.available);
  workstationsMap.set('01-01-01-14', UserRectStatus.available);
  workstationsMap.set('01-01-01-15', UserRectStatus.available);
  workstationsMap.set('01-01-01-16', UserRectStatus.available);

  const { loading, workstations } = useUserMap(workstationsMap);

  return (
    <MapInteractionCSS>
      { !loading && (
        <svg xmlns="http://www.w3.org/2000/svg" version="1.1" width="827px" height="1169px" viewBox="-0.5 -0.5 827 1169">
        <defs />
        <g>
          <Rect x="40" y="40" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-01-01-01')} name="01-01-01-01" strokeWidth='1' />
          <Rect x="240" y="40" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-01-01-02')} name="01-01-01-02" strokeWidth='1' />
          <Rect x="440" y="40" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-01-01-03')} name="01-01-01-03" strokeWidth='1' />
          <Rect x="640" y="40" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-01-01-04')} name="01-01-01-04" strokeWidth='1' />
          <Rect x="40" y="360" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-01-01-05')} name="01-01-01-05" strokeWidth='1' />
          <Rect x="240" y="360" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-01-01-06')} name="01-01-01-06" strokeWidth='1' />
          <Rect x="440" y="360" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-01-01-07')} name="01-01-01-07" strokeWidth='1' />
          <Rect x="640" y="360" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-01-01-08')} name="01-01-01-08" strokeWidth='1' />
          <Rect x="40" y="680" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-01-01-09')} name="01-01-01-09" strokeWidth='1' />
          <Rect x="240" y="680" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-01-01-10')} name="01-01-01-10" strokeWidth='1' />
          <Rect x="440" y="680" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-01-01-11')} name="01-01-01-11" strokeWidth='1' />
          <Rect x="640" y="680" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-01-01-12')} name="01-01-01-12" strokeWidth='1' />
          <Rect x="40" y="1000" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-01-01-13')} name="01-01-01-13" strokeWidth='1' />
          <Rect x="240" y="1000" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-01-01-14')} name="01-01-01-14" strokeWidth='1' />
          <Rect x="440" y="1000" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-01-01-15')} name="01-01-01-15" strokeWidth='1' />
          <Rect x="640" y="1000" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-01-01-16')} name="01-01-01-16" strokeWidth='1' />
        </g>
      </svg>
      ) }
    </MapInteractionCSS>
  );
}
