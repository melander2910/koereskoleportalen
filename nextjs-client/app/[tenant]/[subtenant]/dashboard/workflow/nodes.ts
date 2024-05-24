
import { Node } from 'reactflow';

export const initialNodes : Node[] = [
    { id: '1', type: 'input', position: { x: 100, y: 50 }, data: { label: 'Register new student' }, style: {height: 40, width: 150}},
    { id: '2', position: { x: 50, y: 400 }, data: { label: 'Add student to course' } },
    { id: '3', type: 'output', position: { x: 300, y: 500 }, data: { label: 'First driving lesson' } },
];