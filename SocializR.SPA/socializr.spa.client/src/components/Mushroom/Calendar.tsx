import { Col, Row } from "react-bootstrap";
import "./Calendar.css";
import { Luna } from "../../types/types";

interface CalendarProps {
    perioada: number[]
}

const Calendar = ({ perioada }: CalendarProps) => {
    let months = [
        {
            name: Luna.ianuarie,
            display: "I"
        },
        {
            name: Luna.februarie,
            display: "II"
        },
        {
            name: Luna.martie,
            display: "III"
        },
        {
            name: Luna.aprilie,
            display: "IV"
        },
        {
            name: Luna.mai,
            display: "V"
        },
        {
            name: Luna.iunie,
            display: "VI"
        },
        {
            name: Luna.iulie,
            display: "VII"
        },
        {
            name: Luna.august,
            display: "VIII"
        },
        {
            name: Luna.septembrie,
            display: "IX"
        },
        {
            name: Luna.octombrie,
            display: "X"
        },
        {
            name: Luna.noiembrie,
            display: "XI"
        },
        {
            name: Luna.decembrie,
            display: "XII"
        },
    ]

    const monthStart = perioada[0];
    const monthEnd = perioada[1];
    const currentMonth = new Date().getMonth();
    const allYearRound = (monthStart === 1 && monthEnd === 12) || (monthEnd === 1 && monthStart === 12);
    const isInSeason = currentMonth + 1 >= perioada[0] && currentMonth + 1 <= perioada[1];

    function colorMonth(month: number) {
        if (monthStart < monthEnd) {
            const indexStart = monthStart - 1;
            const indexEnd = monthEnd - 1;

            if (month === indexStart) {
                return 'bg-start-of-season';
            } else if (month === indexEnd) {
                return 'bg-end-of-season';
            } else if (month > indexStart && month < indexEnd) {
                return 'bg-in-season';
            } else return 'bg-out-of-season';
        } else {
            const indexStart = monthEnd - 1;
            const indexEnd = monthStart - 1;

            if (month === indexStart) {
                return 'bg-end-of-season';
            } else if (month === indexEnd) {
                return 'bg-start-of-season';
            } else if (month > indexStart && month < indexEnd) {
                return 'bg-out-of-season';
            } else return 'bg-in-season';
        }
    }

    return (
        <Row>
            {months.map((month, index) => (
                <Col xs={2} sm={1} lg={1} key={index} title={Object.values(Luna)[index]} 
                    className={`${allYearRound ? 'bg-in-season' : colorMonth(index)} text-center border border-secondary`}>
                    <span className={index === currentMonth ? isInSeason ? "fw-bold text-success" : "fw-bold text-danger" : ""}>{month.display}</span>
                </Col>
            ))}
        </Row>
    )
};

export default Calendar;