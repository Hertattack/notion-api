import type { Meta, StoryObj } from '@storybook/react';

import { Node } from './Node.tsx';

const meta: Meta<typeof Node> = {
    component: Node,
};

export default meta;

type Story = StoryObj<typeof Node>;

export const Primary: Story = {
    name: "Simple Example",
    render: () => <Node>Hello World!<br/>Moar!</Node>
};