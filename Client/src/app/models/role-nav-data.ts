import { INavData } from '@coreui/angular';

export interface IRoleNavData extends INavData {
  roles?: string[]; // Add roles property for access control
  dynamicUrl?: (id: number) => string;
}