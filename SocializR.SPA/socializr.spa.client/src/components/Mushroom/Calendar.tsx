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
            display: "ian"
        },
        {
            name: Luna.februarie,
            display: "feb"
        },
        {
            name: Luna.martie,
            display: "mar"
        },
        {
            name: Luna.aprilie,
            display: "apr"
        },
        {
            name: Luna.mai,
            display: "mai"
        },
        {
            name: Luna.iunie,
            display: "iun"
        },
        {
            name: Luna.iulie,
            display: "iul"
        },
        {
            name: Luna.august,
            display: "aug"
        },
        {
            name: Luna.septembrie,
            display: "sep"
        },
        {
            name: Luna.octombrie,
            display: "oct"
        },
        {
            name: Luna.noiembrie,
            display: "nov"
        },
        {
            name: Luna.decembrie,
            display: "dec"
        },
    ]

    const allYearRound = perioada[0] === 1 && perioada[1] === 12;

    function colorMonth(month: number) {
        const indexStart = perioada[0] - 1;
        const indexEnd = perioada[1] - 1;

        if (month === indexStart) {
            return 'bg-start-of-season';
        } else if (month === indexEnd) {
            return 'bg-end-of-season';
        } else if (month > indexStart && month < indexEnd) {
            return 'bg-in-season';
        } else return 'bg-out-of-season';
    }

    return (
        <Row>
            {months.map((month, index) => (
                <Col className={`${allYearRound ? 'bg-in-season' : colorMonth(index)} text-center border border-secondary`}>
                    <span>{month.display}</span>
                </Col>
            ))}
        </Row>
    )
};

export default Calendar;