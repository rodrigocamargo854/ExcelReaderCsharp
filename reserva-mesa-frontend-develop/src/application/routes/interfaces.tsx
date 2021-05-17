import React from 'react';
import { PermissionTypeEnum } from './permissionTypes';

export interface BroadcrumpProps {
  text: string | undefined;
  link?: string | undefined;
}
export interface RouteProperties {
  permission?: PermissionTypeEnum[];
  component: React.FC<{}>;
  exact?: boolean;
  path: string;
  breadcrumbName?: string;
  breadcrumbList?: BroadcrumpProps[] | undefined;
  children?: RouteProperties[];
}

export interface MenuProperties{
  permission: PermissionTypeEnum[],
  key: string,
  path: string,
  icon: string,
  label: string
}
