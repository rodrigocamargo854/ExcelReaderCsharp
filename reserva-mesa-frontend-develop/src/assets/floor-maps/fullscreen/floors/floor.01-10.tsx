import React from 'react';
import { MapInteractionCSS } from 'react-map-interaction';

import Rect from '../FullscreenRect';
import { FullscreenRectStatus } from '../enums';
import useUserMap from '../hooks/useFullscreenMap';

export default function SvgComponent(props: React.SVGProps<SVGSVGElement>) {
  const workstationsMap = new Map();
  workstationsMap.set('01-10-01-01', FullscreenRectStatus.available);
  workstationsMap.set('01-10-01-02', FullscreenRectStatus.available);
  workstationsMap.set('01-10-01-03', FullscreenRectStatus.available);
  workstationsMap.set('01-10-01-04', FullscreenRectStatus.available);
  workstationsMap.set('01-10-01-05', FullscreenRectStatus.available);
  workstationsMap.set('01-10-01-06', FullscreenRectStatus.available);
  workstationsMap.set('01-10-01-07', FullscreenRectStatus.available);
  workstationsMap.set('01-10-01-08', FullscreenRectStatus.available);
  workstationsMap.set('01-10-01-09', FullscreenRectStatus.available);
  workstationsMap.set('01-10-01-10', FullscreenRectStatus.available);
  workstationsMap.set('01-10-01-11', FullscreenRectStatus.available);
  workstationsMap.set('01-10-01-12', FullscreenRectStatus.available);
  workstationsMap.set('01-10-01-13', FullscreenRectStatus.available);
  workstationsMap.set('01-10-01-14', FullscreenRectStatus.available);
  workstationsMap.set('01-10-01-15', FullscreenRectStatus.available);
  workstationsMap.set('01-10-01-16', FullscreenRectStatus.available);

  const { loading, workstations } = useUserMap(workstationsMap);

  return (
    <MapInteractionCSS>
      { !loading && (
        <svg xmlns="http://www.w3.org/2000/svg" version="1.1" width="827px" height="1169px" viewBox="-0.5 -0.5 827 1169">
        <defs />
        <g>
          <Rect x="40" y="40" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-10-01-01')} name="01-10-01-01" strokeWidth='1' />
          <Rect x="240" y="40" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-10-01-02')} name="01-10-01-02" strokeWidth='1' />
          <Rect x="440" y="40" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-10-01-03')} name="01-10-01-03" strokeWidth='1' />
          <Rect x="640" y="40" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-10-01-04')} name="01-10-01-04" strokeWidth='1' />
          <Rect x="40" y="360" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-10-01-05')} name="01-10-01-05" strokeWidth='1' />
          <Rect x="240" y="360" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-10-01-06')} name="01-10-01-06" strokeWidth='1' />
          <Rect x="440" y="360" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-10-01-07')} name="01-10-01-07" strokeWidth='1' />
          <Rect x="640" y="360" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-10-01-08')} name="01-10-01-08" strokeWidth='1' />
          <Rect x="40" y="680" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-10-01-09')} name="01-10-01-09" strokeWidth='1' />
          <Rect x="240" y="680" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-10-01-10')} name="01-10-01-10" strokeWidth='1' />
          <Rect x="440" y="680" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-10-01-11')} name="01-10-01-11" strokeWidth='1' />
          <Rect x="640" y="680" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-10-01-12')} name="01-10-01-12" strokeWidth='1' />
          <Rect x="40" y="1000" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-10-01-13')} name="01-10-01-13" strokeWidth='1' />
          <Rect x="240" y="1000" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-10-01-14')} name="01-10-01-14" strokeWidth='1' />
          <Rect x="440" y="1000" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-10-01-15')} name="01-10-01-15" strokeWidth='1' />
          <Rect x="640" y="1000" width="160" height="160" stroke="#000000" pointer-events="all" status={workstations.get('01-10-01-16')} name="01-10-01-16" strokeWidth='1' />
        </g>
      </svg>
      ) }
    </MapInteractionCSS>
  );
}
