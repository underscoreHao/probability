import React, { useState } from "react";
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import RangeSlider from 'react-bootstrap-range-slider';
import ButtonGroup from 'react-bootstrap/ButtonGroup';
import ToggleButton from 'react-bootstrap/ToggleButton';

const Main = () => {
  // Depending on use case we might store everything in a single state object
  const [probability, setProbability] = useState({});
  const [valueA, setValueA] = useState(0);
  const [valueB, setValueB] = useState(0);
  const [typeValue, setTypeValue] = useState('1');
  const [logs, setLogs] = useState([]);

  const radios = [
    { name: 'And', value: '0' },
    { name: 'Or', value: '1' },
  ];

  async function onCalculate() {
    const request = {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ a: valueA, b: valueB, type: typeValue === '0' ? 'And' : 'Or' })
    };

    const response = await fetch('calculateProbability', request);
    const data = await response.json();
    setProbability(data);

    setLogs([...logs, data.log]);
  };

  function isInRange(value) {
    return value >= 0.0 && value <= 1.0
  }

  // Most of the rows should be separate components, but in order to save time I've put everything in a single component
  return (
    <>
      <Container>
        <Row className="align-items-center mt-3">
          <h1>Probability Calculator</h1>
        </Row>
        <Row className="align-items-center mt-5">
          <Col md="8">
            <Form>
              <Form.Group as={Row}>
                <Col>
                  <h3>A</h3>
                  <RangeSlider
                    value={valueA}
                    onChange={e => setValueA(e.target.value)}
                    step={0.1}
                    min={0.0}
                    max={1.0}
                  />
                  <Form.Control
                    value={valueA}
                    onChange={e => isInRange(e.target.value) ? setValueA(e.target.value) : 0.0}
                  />
                </Col>
                <Col>
                  <h3>B</h3>
                  <RangeSlider
                    value={valueB}
                    onChange={e => setValueB(e.target.value)}
                    step={0.1}
                    min={0.0}
                    max={1.0}
                  />
                  <Form.Control
                    value={valueB}
                    onChange={e => isInRange(e.target.value) ? setValueB(e.target.value) : 0.0}
                  />
                </Col>
              </Form.Group>
            </Form>
          </Col>
          <Col md="4">
            <h3>Probability Type</h3>
            <ButtonGroup className="mt-3">
              {radios.map((radio, idx) => (
                <ToggleButton
                  key={idx}
                  id={`radio-${idx}`}
                  type="radio"
                  size="lg"
                  variant={idx % 2 ? 'outline-success' : 'outline-danger'}
                  name="radio"
                  value={radio.value}
                  checked={typeValue === radio.value}
                  onChange={(e) => setTypeValue(e.currentTarget.value)}
                >
                  {radio.name}
                </ToggleButton>
              ))}
            </ButtonGroup>
          </Col>
        </Row>
        <Row className="mt-5">
          <Col>
            <Button variant="primary" size="lg" onClick={onCalculate}>Calculate</Button>{' '}
          </Col>
        </Row>
        <Row className="mt-5">
          <Col>
            <h3>Result: {probability.result}</h3>
          </Col>
        </Row>
        <Row className="mt-5">
          <Row>
            <Form>
              <Form.Group className="mb-3" controlId="exampleForm.ControlTextarea1">
                <Form.Label>Calculation Log</Form.Label>
                <Form.Control
                  as="textarea"
                  rows={3}
                  value={logs.map(l => l + `\n`).join('')}
                  disabled />
              </Form.Group>
            </Form>
          </Row>
        </Row>
      </Container>
    </>
  )
}

export default Main;
