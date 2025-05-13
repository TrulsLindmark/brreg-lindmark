import {
  ICompanyOrganizationTypeDistribution,
  IOrganizationTypeDistribution,
} from "../../data/models/companySummary";
import { DoughtnutChart } from "../DoughnutChart/DoughnutChart";
import "./OrganizationTypeDistribution.scss";

interface IProps {
  organizationTypeDistribution: ICompanyOrganizationTypeDistribution;
}

export function OrganizationTypeDistribution(props: IProps) {
  const { organizationTypeDistribution } = props;

  // Looks better when it is sorted in the Donut chart
  const sortedOrganizationTypeDistribution =
    organizationTypeDistribution.organizationTypeDistribution.sort(
      (a: IOrganizationTypeDistribution, b: IOrganizationTypeDistribution) =>
        b.percentage - a.percentage
    );

  let dataPoints: number[] = [];
  let labels: string[] = [];
  sortedOrganizationTypeDistribution.forEach(
    (item: IOrganizationTypeDistribution) => {
      dataPoints.push(item.percentage);
      labels.push(item.code);
    }
  );

  const data = {
    labels: labels,
    datasets: [
      {
        label: "Prosent",
        data: dataPoints,
        backgroundColor: [
          "#ff6384",
          "rgba(190, 235, 54, 0.2)",
          "rgba(255, 206, 86, 0.2)",
          "rgba(75, 192, 192, 0.2)",
        ],
        borderColor: [
          "rgba(255, 99, 132, 1)",
          "rgba(54, 162, 235, 1)",
          "rgba(255, 206, 86, 1)",
          "rgba(75, 192, 192, 1)",
        ],
        borderWidth: 1,
      },
    ],
  };

  return (
    <div>
      <h2>Prosentvis fordeling av organisasjonsformer</h2>
      <div className="organization-type-distribution">
        <div className="organization-type-distribution__chart">
          <DoughtnutChart data={data} />
        </div>
      </div>
    </div>
  );
}
