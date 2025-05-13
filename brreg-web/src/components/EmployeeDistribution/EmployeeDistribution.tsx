import { ICompanyEmployeeDistribution } from "../../data/models/companySummary";
import { BarChart } from "../BarChart/BarChart";

interface IProps {
  employeeDistribution: ICompanyEmployeeDistribution;
}

export function EmployeeDistribution(props: IProps) {
  const { employeeDistribution } = props;

  const translations: Record<string, string> = {
    zeroOrNull: "0 ansatte",
    oneToNine: "1-9 ansatte",
    tenToFortyNine: "10-49 ansatte",
    moreThanFifty: "50+ ansatte",
  };

  let dataPoints: number[] = [];
  let labels: string[] = [];
  for (const [key, value] of Object.entries(employeeDistribution)) {
    dataPoints.push(value);
    labels.push(translations[key]);
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
      <h2>Fordeling av antall ansatte</h2>
      <div className="employee-distribution">
        <div className="employee-distribution__chart">
          <BarChart data={data} />
        </div>
      </div>
    </div>
  );
}
