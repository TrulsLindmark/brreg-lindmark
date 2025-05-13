export interface ICompanySummary {
  statusCount: ICompanyStatusCount;
  employeeDistribution: ICompanyEmployeeDistribution;
  organizationTypeDistribution: ICompanyOrganizationTypeDistribution;
}

export interface ICompanyStatusCount {
  aktiv: number;
  underAvvikling: number;
  konkurs: number;
  slettet: number;
  feil: number;
}

export interface ICompanyEmployeeDistribution {
  zeroOrNull: number;
  oneToNine: number;
  tenToFortyNine: number;
  moreThanFifty: number;
}

export interface ICompanyOrganizationTypeDistribution {
  organizationTypeDistribution: IOrganizationTypeDistribution[];
}

export interface IOrganizationTypeDistribution {
  code: string;
  percentage: number;
}
