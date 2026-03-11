import { mkdir, cp, rm } from 'node:fs/promises';

await rm('build/web', { recursive: true, force: true });
await mkdir('build/web', { recursive: true });
await cp('ui/public', 'build/web', { recursive: true });

console.log('Build web gerado em build/web');
