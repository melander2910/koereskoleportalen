'use client'
import React, { useCallback, useState } from 'react';
import ReactFlow, { addEdge, useEdgesState, useNodesState, MiniMap,
  Controls,
  Background,
  BackgroundVariant,
  Panel, } from 'reactflow';
import {initialNodes} from './nodes'
import 'reactflow/dist/style.css';

const initialEdges = [
  { id: 'e1-1', source: '1', target: '2' }, 
  { id: 'e2-2', source: '2', target: '3' },
];


export default function page() {
  const [size, setSize] = useState({height: 800, width: 500});
  const [nodes, setNodes, onNodesChange] = useNodesState(initialNodes);
  const [edges, setEdges, onEdgesChange] = useEdgesState(initialEdges);

  let test = {animated: true}
  const onConnect = useCallback(
    (params: any) => setEdges((eds) => addEdge({...params, animated: test.animated, label: 'Student is on course'}, eds)),
    [setEdges],
  );
  
  const ResizeDiagram = (height : number, width : number) => {
    if(height == size.height && width == size.width){
      height = 800;
      width = 500;
    }
    setSize({height: height, width: width})
  }

  return (
    <>
    <div style={{ width: size.width, height: size.height, border: '1px solid black' }}>
      <ReactFlow
      nodes={nodes} 
      edges={edges} 
      onNodesChange={onNodesChange}
      onEdgesChange={onEdgesChange}
      onConnect={onConnect}
      // fitView
      >
      <Panel position="top-left"><button onClick={() => ResizeDiagram(400, 400)} className="border-4">Resize</button></Panel>

         <Controls />
        <MiniMap />
        <Background variant={BackgroundVariant.Lines} gap={24} />
      </ReactFlow>
      
    </div>
      </>
  )
}
