import { ICompanyStatusCount } from "../../data/models/companySummary";
import { BarChart } from "../BarChart/BarChart";

interface IProps {
  statusDistribution: ICompanyStatusCount;
}

export function StatusDistribution(props: IProps) {
  const { statusDistribution } = props;

  let dataPoints: number[] = [];
  let labels: string[] = [];
  for (const [key, value] of Object.entries(statusDistribution)) {
    dataPoints.push(value);
    labels.push(key);
  }

  const data = {
    labels: labels,
    datasets: [
      {
        label: "Antall",
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
      <h2>Antall for hver status</h2>
      <div className="status-distribution">
        <div className="status-distribution__chart">
          <BarChart data={data} />
        </div>
      </div>
    </div>
  );
}
