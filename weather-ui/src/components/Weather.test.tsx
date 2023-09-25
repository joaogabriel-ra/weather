import { render, screen } from "@testing-library/react";
import Weather from "./Weather";
import "@testing-library/jest-dom";

describe("Weather Component", () => {
  it("renders without errors", () => {
    render(<Weather />);
    expect(screen.getByText("7-Day Weather Forecast")).toBeInTheDocument();
  });
});
